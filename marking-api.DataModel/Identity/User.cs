using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Project;
using Microsoft.AspNetCore.Identity;

namespace marking_api.DataModel.Identity
{
    /// <summary>
    /// User data model
    /// </summary>
    [GeneratedController("api/user")]
    [Table("IdUsers", Schema = "dbo")]
    public class User : IdentityUser
    {
        /// <summary>
        /// If user is disabled
        /// </summary>
        [PersonalData]
        public bool IsDisabled { get; set; }
        /// <summary>
        /// If user is deleted
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// Profile picture of the user
        /// </summary>
        public byte[] ProfilePicture { get; set; } = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAADIAAAAyBAMAAADsEZWCAAAAElBMVEXR0dGqqqrCwsKysrK5ubnKysqGpJ8RAAAAtUlEQVQ4y72TMQ7CMAxFTaAH+CnsKRI7QepOBvZWgvtfBaESSv1jKRKob32K/W238iOP40lKuATAC9NEvLiy6TBxoSdvwa+2WVCrER+USbMJSxNhNcLMeW3D2UwTzEkHWnXG3FsrtdVg760qdW22nXltSRSNyolmVIPyZ+XJNOo6HGEQo9xeLOP/ZDZmH9t0VmqHiZ5WkNThWKBdKBfxxa1wUP3n34GycmD6vH4m5CbEIc/OyBPDUSvwZuB80QAAAABJRU5ErkJggg==");
        /// <summary>
        /// First name of the user
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of the user
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// List of user claims
        /// </summary>
        [SwaggerExclude]
        public virtual ICollection<UserClaim> UserClaims { get; set; }
        /// <summary>
        /// List of user logins
        /// </summary>
        [SwaggerExclude]
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        /// <summary>
        /// List of user roles
        /// </summary>
        [SwaggerExclude]
        public virtual ICollection<UserRole> UserRoles { get; set; }
        /// <summary>
        /// List of user tokens
        /// </summary>
        [SwaggerExclude]
        public virtual ICollection<UserToken> UserTokens { get; set; }
        /// <summary>
        /// List of user tags
        /// </summary>
        [SwaggerExclude]
        public virtual ICollection<TagDM> UserTags { get; set; }
    }
}
