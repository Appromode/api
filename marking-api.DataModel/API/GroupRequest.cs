using marking_api.DataModel.Identity;
using System.Collections.Generic;

namespace marking_api.DataModel.API
{
    /// <summary>
    /// Used to post back key information about a group to create one
    /// </summary>
    public class GroupRequest
    {
        /// <summary>
        /// Id of the sender/logged in user
        /// </summary>
        public string SenderId;
        /// <summary>
        /// Name of the group
        /// </summary>
        public string GroupName;
        /// <summary>
        /// Description of the group
        /// </summary>
        public string GroupDescription;
        /// <summary>
        /// List of group members
        /// </summary>
        public List<User> GroupMembers;
    }
}
