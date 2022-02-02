using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using marking_api.DataModel.CustomAttributes;
using Microsoft.AspNetCore.Identity;

namespace marking_api.DataModel.Identity
{
    [GeneratedController("api/user")]
    [Table("IdUsers", Schema = "dbo")]
    public class User : IdentityUser
    {
        [PersonalData]
        public bool IsDisabled { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] ProfilePicture { get; set; } = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAADIAAAAyBAMAAADsEZWCAAAAElBMVEXR0dGqqqrCwsKysrK5ubnKysqGpJ8RAAAAtUlEQVQ4y72TMQ7CMAxFTaAH+CnsKRI7QepOBvZWgvtfBaESSv1jKRKob32K/W238iOP40lKuATAC9NEvLiy6TBxoSdvwa+2WVCrER+USbMJSxNhNcLMeW3D2UwTzEkHWnXG3FsrtdVg760qdW22nXltSRSNyolmVIPyZ+XJNOo6HGEQo9xeLOP/ZDZmH9t0VmqHiZ5WkNThWKBdKBfxxa1wUP3n34GycmD6vH4m5CbEIc/OyBPDUSvwZuB80QAAAABJRU5ErkJggg==");
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [SwaggerExclude]
        public virtual ICollection<UserClaim> UserClaims { get; set; }
        [SwaggerExclude]
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        [SwaggerExclude]
        public virtual ICollection<UserRole> UserRoles { get; set; }
        [SwaggerExclude]
        public virtual ICollection<UserToken> UserTokens { get; set; }
    }
}
