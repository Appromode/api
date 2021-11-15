using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using marking_api.DataModel.CustomAttributes;

namespace marking_api.DataModel.Identity
{
    [GeneratedController("api/roleclaim")]
    [Table("IdRoleClaims", Schema = "dbo")]
    public class RoleClaim : IdentityRoleClaim<string>
    {
        public RoleClaim() : base() { }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
