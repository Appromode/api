using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.API
{
    /// <summary>
    /// Refresh token class. Used to regen a jwt token
    /// </summary>
    [Table("RefreshToken")]
    public class RefreshTokenDM
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 RefreshTokenId { get; set; }

        /// <summary>
        /// User that generated the refresh token
        /// </summary>
        [ForeignKey("UserId")]
        public virtual string UserId { get; set; }
        /// <summary>
        /// User Object
        /// </summary>
        [SwaggerExclude]
        public virtual User User { get; set; }

        /// <summary>
        /// Refresh token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Jwt token id
        /// </summary>
        public string JwtId { get; set; }
        /// <summary>
        /// If the refresh token has been used
        /// </summary>
        public bool IsUsed { get; set; }
        /// <summary>
        /// If the refresh token has been revoked
        /// </summary>
        public bool IsRevoked { get; set; }
        /// <summary>
        /// When the refresh token was generated
        /// </summary>
        public DateTime AddedDate { get; set; }
        /// <summary>
        /// When the refresh token expires. Currently set to 6 months.
        /// </summary>
        public DateTime ExpiryDate { get; set; }
    }
}
