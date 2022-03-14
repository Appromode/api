using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Global.Extensions
{
    /// <summary>
    /// MVC model modelstate extension methods
    /// </summary>
    public static class ModelStateExtensions
    {
        /// <summary>
        /// Get error messages from modelstate of MVC model
        /// </summary>
        /// <param name="dictionary">Modelstate of the MVC model</param>
        /// <returns>String list of error messages of the modelstate</returns>
        public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
        {
            return dictionary.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToList();
        }
    }
}
