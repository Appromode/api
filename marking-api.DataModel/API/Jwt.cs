namespace marking_api.DataModel.API
{
    /// <summary>
    /// JWT secret class
    /// </summary>
    public class Jwt
    {
        /// <summary>
        /// Used to bind the jwt secret in the configuration against
        /// </summary>
        public string Secret { get; set; }
    }
}
