using System.ComponentModel.DataAnnotations;

namespace marking_api.DataModel.API
{
    /// <summary>
    /// Token class used to regenerate jwt token
    /// </summary>
    public class TokenRequest
    {
        /// <summary>
        /// JWT bearer token used for authentication
        /// </summary>
        [Required]
        public string BearerToken { get; set; }
        /// <summary>
        /// Refresh token used to regenerate jwt tokens
        /// </summary>
        [Required]
        public string RefreshToken { get; set; }
    }
}
