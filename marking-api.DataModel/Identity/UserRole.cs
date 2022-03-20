using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using marking_api.DataModel.CustomAttributes;

namespace marking_api.DataModel.Identity
{
    /// <summary>
    /// User role data model
    /// </summary>
    [GeneratedController("api/userrole")]
    [Table("IdUserRoles", Schema = "dbo")]
    public class UserRole : IdentityUserRole<string>
    {
        /// <summary>
        /// User role constructor inheriting base class
        /// </summary>
        public UserRole() : base() { }

        /// <summary>
        /// Override role id to string
        /// </summary>
        [ForeignKey("RoleId")]
        public override string RoleId { get; set; }
        /// <summary>
        /// Role object
        /// </summary>
        [SwaggerExclude]        
        public virtual Role Role { get; set; }

        /// <summary>
        /// Override user id to string
        /// </summary>
        [ForeignKey("UserId")]
        public override string UserId { get; set; }
        /// <summary>
        /// User object
        /// </summary>
        [SwaggerExclude]        
        public virtual User User { get; set; }
    }
}
