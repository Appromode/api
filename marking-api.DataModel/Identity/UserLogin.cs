using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using marking_api.DataModel.CustomAttributes;

namespace marking_api.DataModel.Identity
{
    [GeneratedController("api/userlogin")]
    [Table("IdUserLogins", Schema = "dbo")]
    public class UserLogin : IdentityUserLogin<string>
    {
        public UserLogin() : base() { }

        [SwaggerExclude]
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
