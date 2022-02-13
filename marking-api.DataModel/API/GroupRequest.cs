using marking_api.DataModel.Identity;
using System.Collections.Generic;

namespace marking_api.DataModel.API
{
    public class GroupRequest
    {
        public string GroupName;
        public string GroupDescription;
        public List<User> GroupMembers;
    }
}
