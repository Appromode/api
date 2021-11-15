using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using marking_api.DataModel.CustomAttributes;

namespace marking_api.DataModel.Identity
{
    [GeneratedController("api/usertoken")]
    [Table("IdUserTokens", Schema = "dbo")]
    public class UserToken : IdentityUserToken<string>
    {
        public UserToken() : base() { }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
