﻿using marking_api.DataModel.Identity;
using marking_api.Global.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace marking_api.Global.Services
{
    /// <summary>
    /// Utility service that contains useful methods that are used throughout the application
    /// </summary>
    public class UtilService
    {
        //Used to access the database
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Setup unitofwork
        /// </summary>
        /// <param name="unitOfWork">Injected to access the database and include extra methods</param>
        public UtilService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Check whether a date is null, empty or blank
        /// </summary>
        /// <param name="date">Nullable DateTime</param>
        /// <returns>True if date is null, empty or blank</returns>
        public bool IsDateTimeNullOrEmpty(DateTime? date)
        {
            return !date.HasValue ? true : false;
        }

        /// <summary>
        /// Checks whether a user is disabled
        /// </summary>
        /// <param name="userName">Name of a user</param>
        /// <returns>True if user is disabled</returns>
        public bool IsUserDisabled(string userName)
        {
            return _unitOfWork.Users.Get(filter: x => x.UserName.Equals(userName)).FirstOrDefault().IsDisabled;
        }

        /// <summary>
        /// Generates a list of menu links depending on the roles the user has.
        /// Uses security strings in the links table and sitearea table to use as permissions
        /// </summary>
        /// <param name="userName">Name of a user</param>
        /// <returns>List of menu links the user has access to</returns>
        public List<LinkDM> GenerateUserMenu(string userName)
        {
            List<LinkDM> userLinks;
            List<LinkDM> userMenus = new List<LinkDM>();
            var userRoles = _unitOfWork.UserRoles.Get(include: x => x.Include(y => y.User).Include(y => y.Role), filter: x => x.User.UserName.Equals(userName)).ToList();
            if (userRoles != null)
                userLinks = _unitOfWork.Links.Get(filter: x => userRoles.Any(y => y.Role.AccessRole.Equals(x.AccessRole))).ToList();
            else
                return null;

            if (userLinks != null)
            {
                var topmenus = userLinks.Where(x => x.LinkParentId == 0 || x.LinkParentId == null);                
                foreach (var menu in topmenus.OrderBy(x => x.LinkPosition))
                {
                    menu.LinkChildren = null;
                    var top = menu;
                    RecurseChildLinks(userLinks, menu.LinkId, top);
                    if (top.LinkChildren?.Any() == true || top.LinkUrl != null)
                        userMenus.Add(top);
                }
            }
            return userMenus;
        }

        /// <summary>
        /// Recurses through all links and generates a list of child links to assign to parentlink object
        /// </summary>
        /// <param name="userLinks">List of all links in the database</param>
        /// <param name="ParentLinkId">Id of the parent link</param>
        /// <param name="ParentLink">Parent link object</param>
        public void RecurseChildLinks(List<LinkDM> userLinks, Int64 ParentLinkId, LinkDM ParentLink)
        {
            var childlinks = userLinks.Where(x => x.LinkParentId == ParentLinkId);
            foreach (var child in childlinks.OrderBy(x => x.LinkPosition))
            {
                child.LinkChildren = null;
                if (ParentLink.LinkChildren == null)
                    ParentLink.LinkChildren = new List<LinkDM>();

                ParentLink.LinkChildren.Add(child);
                RecurseChildLinks(userLinks, child.LinkId, child);
            }
        }
    }
}
