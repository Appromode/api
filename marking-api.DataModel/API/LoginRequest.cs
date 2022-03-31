using System.ComponentModel.DataAnnotations;

namespace marking_api.DataModel.API
{
    /// <summary>
    /// Class used to recieve login information
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Email of the user trying to login
        /// </summary>
        [Required]
        public string Email { get; set; }
        /// <summary>
        /// Password of the user trying to login
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
