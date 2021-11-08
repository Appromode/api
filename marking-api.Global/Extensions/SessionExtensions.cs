using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace marking_api.Global.Extensions
{
    public static class SessionExtensions
    {
        private static JsonSerializerSettings _settings = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects
        };

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value, _settings);
        }

        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value, _settings));
        }
    }
}
