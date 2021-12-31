using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
