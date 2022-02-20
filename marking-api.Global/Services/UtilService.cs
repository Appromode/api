﻿using marking_api.DataModel.Identity;
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

        public List<LinkDM> GenerateUserMenu(string userId)
        {
            //List<LinkDM> userLinks = _unitOfWork.Links.Get(include: x => x.Include());
            return null; //Generate nav menu for user
        }

        public void RecurseChildLinks(List<LinkDM> userLinks, Int64 ParentLinkId, LinkDM ParentLink)
        {
            //Recurse child links per parent link
        }
    }
}
