using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Enums;

namespace marking_api.DataModel.Identity
{
    [GeneratedController("api/role")]
    [Table("IdRoles", Schema = "dbo")]
    public class Role : IdentityRole
    {
        [StringLength(255)]
        public string RoleDescription { get; set; }

        [SwaggerExclude]
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }
        [SwaggerExclude]
        public virtual ICollection<UserRole> UserRoles { get; set; }
        [SwaggerExclude]
        public virtual ICollection<RolePermission> RolePermissions { get; set; }

        public RoleType AccessRole { get; set; } = RoleType.Guest;
    }
}
