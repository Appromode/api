using System.ComponentModel.DataAnnotations;

namespace marking_api.DataModel.API
{
    public class TokenRequest
    {
        [Required]
        public string BearerToken { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
