using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using marking_api.DataModel.CustomAttributes;

namespace marking_api.DataModel.Identity
{
    [GeneratedController("api/role")]
    [Table("IdRoles", Schema = "dbo")]
    public class Role : IdentityRole
    {
        [StringLength(255)]
        public string RoleDescription { get; set; }

        public virtual ICollection<RoleClaim> RoleClaims { get; set; }
    }
}
