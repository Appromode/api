using marking_api.DataModel.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Global.Services
{
    public class UtilService
    {
        public bool IsDateTimeNullOrEmpty(DateTime? date)
        {
            return !date.HasValue ? true : false;
        }

        public List<LinkDM> GenerateUserMenu()
        {
            return null;
        }

        public void RecurseChildLinks()
        {

        }
    }
}
