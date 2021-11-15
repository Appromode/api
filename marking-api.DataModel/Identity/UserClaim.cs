using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using marking_api.DataModel.CustomAttributes;

namespace marking_api.DataModel.Identity
{
    [GeneratedController("api/userclaim")]
    [Table("IdUserClaims", Schema = "dbo")]
    public class UserClaim : IdentityUserClaim<string>
    {
        public UserClaim() : base() { }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
