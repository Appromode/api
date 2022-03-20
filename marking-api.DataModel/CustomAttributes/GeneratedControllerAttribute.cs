using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.DataModel.CustomAttributes
{
    /// <summary>
    /// Attribute class to set the endpoint url of each controller of the api
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class GeneratedControllerAttribute : Attribute
    {
        /// <summary>
        /// Attribute constructor
        /// </summary>
        /// <param name="route">Route of the api endpoint</param>
        public GeneratedControllerAttribute(string route)
        {
            Route = route;
        }

        /// <summary>
        /// API endpoint route
        /// </summary>
        public string Route { get; set; }
    }
}
