using System.Collections.Generic;

namespace marking_api.DataModel.API
{
    /// <summary>
    /// Authorisation request class. Used to authorise a user with the api to request data
    /// </summary>
    public class AuthRequest
    {
        /// <summary>
        /// Jwt token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// The refresh token to regenerate the jwt token if it expires
        /// </summary>
        public string RefreshToken { get; set; }
        /// <summary>
        /// If the auth request was successful
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// List of errors from authorising with the api
        /// </summary>
        public List<string> Errors { get; set; }
    }
}
