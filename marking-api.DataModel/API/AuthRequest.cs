using System.Collections.Generic;

namespace marking_api.DataModel.API
{
    public class AuthRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
