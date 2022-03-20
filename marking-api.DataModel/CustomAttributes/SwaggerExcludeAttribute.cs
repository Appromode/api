using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.DataModel.CustomAttributes
{
    /// <summary>
    /// SwaggerExclude attribute to exclude properties from the swagger tool
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SwaggerExcludeAttribute : Attribute
    {

    }
}
