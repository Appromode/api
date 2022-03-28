using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Enums;

namespace marking_api.DataModel.Identity
{
    /// <summary>
    /// Role data model
    /// </summary>
    [GeneratedController("api/role")]
    [Table("IdRoles", Schema = "dbo")]
    public class Role : IdentityRole
    {
        /// <summary>
        /// Description of the role
        /// </summary>
        [StringLength(255)]
        public string RoleDescription { get; set; }

        /// <summary>
        /// List of role claims
        /// </summary>
        [SwaggerExclude]
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }
        /// <summary>
        /// List of user roles
        /// </summary>
        [SwaggerExclude]
        public virtual ICollection<UserRole> UserRoles { get; set; }
        /// <summary>
        /// List of role permissions
        /// </summary>
        [SwaggerExclude]
        public virtual ICollection<RolePermission> RolePermissions { get; set; }

        /// <summary>
        /// Accessrole enum
        /// </summary>
        public RoleType AccessRole { get; set; } = RoleType.Guest;
    }
}
