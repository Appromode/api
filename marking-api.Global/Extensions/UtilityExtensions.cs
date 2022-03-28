using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Global.Extensions
{
    /// <summary>
    /// Useful extension methods
    /// </summary>
    public static class UtilityExtensions
    {
        /// <summary>
        /// Check if an object contains a property
        /// </summary>
        /// <param name="obj">Type - Object to check for property</param>
        /// <param name="propertyName">string - Name of property within object</param>
        /// <returns>True if object has property</returns>
        public static bool HasProperty(this Type obj, string propertyName)
        {
            return obj.GetProperty(propertyName) != null;
        }

        /// <summary>
        /// Get value of a property from an object generically
        /// </summary>
        /// <typeparam name="T">Generic object to extend from</typeparam>
        /// <param name="obj">object - Object to get property value from</param>
        /// <param name="propertyName">string - Name of the property</param>
        /// <returns>Property value</returns>
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

        /// <summary>
        /// Generate a random byte string as a refresh token
        /// </summary>
        /// <returns>Refresh token string</returns>
        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        /// <summary>
        /// Convert a unix time stamp to date time datatype
        /// </summary>
        /// <param name="unixTimeStamp">long - Unix time stamp to convert</param>
        /// <returns>Converted unix time stamp</returns>
        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp);

            return dateTimeVal;
        }
    }
}
