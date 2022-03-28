using marking_api.DataModel.CustomAttributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;

namespace marking_api.API
{
    /// <summary>
    /// Swagger exclude filter class. Excludes properties marked with this attribute from the swagger tool
    /// </summary>
    public class SwaggerExcludeFilter : ISchemaFilter
    {
        /// <summary>
        /// Apply the filter to properties shown on swagger
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null)
            {
                return;
            }

            var excludedProperties =
                context.Type.GetProperties().Where(
                    t => t.GetCustomAttribute<SwaggerExcludeAttribute>() != null);

            foreach (var excludedProperty in excludedProperties)
            {
                var propertyToRemove =
                    schema.Properties.Keys.SingleOrDefault(
                        x => x.ToLower() == excludedProperty.Name.ToLower());

                if (propertyToRemove != null)
                {
                    schema.Properties.Remove(propertyToRemove);
                }
            }
        }
    }
}
