using System.ComponentModel.DataAnnotations;

namespace marking_api.DataModel.API
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
