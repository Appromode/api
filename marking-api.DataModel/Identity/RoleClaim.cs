using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.DataModel.Identity
{
    [Table("IdRoleClaims", Schema = "dbo")]
    public class RoleClaim : IdentityRoleClaim<string>
    {
        public RoleClaim() : base() { }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
