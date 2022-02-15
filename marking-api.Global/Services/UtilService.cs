using marking_api.DataModel.Identity;
using System;
using System.Collections.Generic;

namespace marking_api.Global.Services
{
    public class UtilService
    {
        public bool IsDateTimeNullOrEmpty(DateTime? date)
        {
            return !date.HasValue ? true : false;
        }

        public List<LinkDM> GenerateUserMenu(string userId)
        {
            return null; //Return list of menus available to the user
        }

        public void RecurseChildLinks(List<LinkDM> menus, LinkDM parentMenu, Int64 parentMenuId)
        {
            //Generate sub menus. One call per parent menu
        }
    }
}
