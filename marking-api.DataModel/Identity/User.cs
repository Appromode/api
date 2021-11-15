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
        public byte[] ProfilePicture { get; set; }

        public virtual ICollection<UserClaim> UserClaims { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }
    }
}
