using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using marking_api.DataModel.CustomAttributes;

namespace marking_api.DataModel.Identity
{
    /// <summary>
    /// Role claim data model
    /// </summary>
    [GeneratedController("api/roleclaim")]
    [Table("IdRoleClaims", Schema = "dbo")]
    public class RoleClaim : IdentityRoleClaim<string>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RoleClaim() : base() { }

        /// <summary>
        /// Role object
        /// </summary>
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
