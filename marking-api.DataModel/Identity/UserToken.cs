using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using marking_api.DataModel.CustomAttributes;

namespace marking_api.DataModel.Identity
{
    /// <summary>
    /// User token data model
    /// </summary>
    [GeneratedController("api/usertoken")]
    [Table("IdUserTokens", Schema = "dbo")]
    public class UserToken : IdentityUserToken<string>
    {
        /// <summary>
        /// Constructor of UserToken inheriting the base class
        /// </summary>
        public UserToken() : base() { }

        /// <summary>
        /// User object because the base doesn't have it
        /// </summary>
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
