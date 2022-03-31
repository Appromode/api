using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using marking_api.DataModel.CustomAttributes;

namespace marking_api.DataModel.Identity
{
    /// <summary>
    /// User claim data model
    /// </summary>
    [GeneratedController("api/userclaim")]
    [Table("IdUserClaims", Schema = "dbo")]
    public class UserClaim : IdentityUserClaim<string>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UserClaim() : base() { }

        /// <summary>
        /// User object
        /// </summary>
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
