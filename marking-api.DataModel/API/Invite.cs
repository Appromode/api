using System.Collections.Generic;
using System;

namespace marking_api.DataModel.API
{
    /// <summary>
    /// Accept invite request class. Used to get post data when a user accepts an invite.
    /// </summary>
    public class Invite
    {
        /// <summary>
        /// Id of invite
        /// </summary>
        public Int64 InviteId { get; set; }
    }
}
