using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using marking_api.DataModel.CustomAttributes;

namespace marking_api.DataModel.Identity
{
    /// <summary>
    /// User login data model
    /// </summary>
    [GeneratedController("api/userlogin")]
    [Table("IdUserLogins", Schema = "dbo")]
    public class UserLogin : IdentityUserLogin<string>
    {
        /// <summary>
        /// User login constructor inheriting from base class
        /// </summary>
        public UserLogin() : base() { }

        /// <summary>
        /// User object
        /// </summary>
        [SwaggerExclude]
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
