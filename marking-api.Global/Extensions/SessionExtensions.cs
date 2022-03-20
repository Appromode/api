using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace marking_api.Global.Extensions
{
    /// <summary>
    /// Session extension which contains methods to deal with sessions and session cookie data.
    /// </summary>
    public static class SessionExtensions
    {
        private static JsonSerializerSettings _settings = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects
        };

        /// <summary>
        /// Get object from json generically
        /// </summary>
        /// <typeparam name="T">Returns any object generically that was set from Json</typeparam>
        /// <param name="session">ISession - Session object</param>
        /// <param name="key">string - Id of the session</param>
        /// <returns>Deserialised Json object</returns>
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value, _settings);
        }

        /// <summary>
        /// Convert object to Json generically
        /// </summary>
        /// <param name="session">ISession - Session object</param>
        /// <param name="key">string - Id of the session</param>
        /// <param name="value">object - Value that is going to be serialised to Json</param>
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value, _settings));
        }
    }
}
