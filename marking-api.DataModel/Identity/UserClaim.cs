using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.DataModel.Identity
{
    [Table("IdUserClaims", Schema = "dbo")]
    public class UserClaim : IdentityUserClaim<string>
    {
        public UserClaim() : base() { }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
