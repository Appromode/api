using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Global.Extensions
{
    public static class UtilityExtensions
    {
        public static bool HasProperty(this Type obj, string propertyName)
        {
            return obj.GetProperty(propertyName) != null;
        }

        public static T GetPropertyValue<T>(this object obj, string propertyName)
        {
            var property = obj.GetType().GetProperty(propertyName);
            if (property == null)
                return default(T);

            var val = property.GetValue(obj, null);

            if (val == null)
                return default(T);

            return (T)property.GetValue(obj, null);
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp);

            return dateTimeVal;
        }
    }
}
