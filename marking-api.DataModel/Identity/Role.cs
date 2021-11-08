using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.DataModel.Identity
{
    [Table("IdRoles", Schema = "dbo")]
    public class Role : IdentityRole
    {
        [StringLength(255)]
        public string RoleDescription { get; set; }

        public virtual ICollection<RoleClaim> RoleClaims { get; set; }
    }
}
