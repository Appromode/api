using marking_api.DataModel.Identity;
using marking_api.Global.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace marking_api.Global.Services
{
    public class UtilService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UtilService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool IsDateTimeNullOrEmpty(DateTime? date)
        {
            return !date.HasValue ? true : false;
        }

        public bool IsUserDisabled(string userName)
        {
            return _unitOfWork.Users.Get(filter: x => x.UserName.Equals(userName)).FirstOrDefault().IsDisabled;
        }

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
