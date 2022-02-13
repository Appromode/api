using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.API
{
    [Table("RefreshToken")]
    public class RefreshTokenDM
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 RefreshTokenId { get; set; }

        [SwaggerExclude]
        [ForeignKey("UserId")]
        public virtual string UserId { get; set; }
        public virtual User User { get; set; }

        public string Token { get; set; }
        public string JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
