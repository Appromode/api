using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using marking_api.DataModel.CustomAttributes;

namespace marking_api.DataModel.Identity
{
    [GeneratedController("api/userrole")]
    [Table("IdUserRoles", Schema = "dbo")]
    public class UserRole : IdentityUserRole<string>
    {
        public UserRole() : base() { }

        [SwaggerExclude]
        public override string RoleId { get; set; }

        [SwaggerExclude]
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        [SwaggerExclude]
        public override string UserId { get; set; }

        [SwaggerExclude]
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
