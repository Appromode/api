using System.Collections.Generic;
using marking_api.DataModel.Project;
using System;

namespace marking_api.DataModel.API
{
    /// <summary>
    /// Accept invite request class. Used to get post data when a user accepts an invite.
    /// </summary>
    public class AddTagRequest
    {
        /// <summary>
        /// Id of invite
        /// </summary>
        public string UserId { get; set; }
        public List<TagDM> tags { get; set; }
    }
}
