using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.Swagger.Filters
{
    internal class SwaggerDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            #region 处理是否必须的排序

            foreach (var schema in swaggerDoc.Components.Schemas)
            {
                if (schema.Value != null && schema.Value.Required.NotNullAndEmpty())
                {
                    var requireds = schema.Value.Required.ToArray();
                    foreach (var item in schema.Value.Properties)
                    {
                        if (requireds.Contains(item.Key))
                        {
                            item.Value.Required = new HashSet<string>() { item.Key };
                        }
                    }

                    schema.Value.Properties = schema.Value.Properties.OrderByDescending(a => a.Value.Required.Count > 0).ToDictionary(p => p.Key, o => o.Value);
                }
            }
            #endregion
        }
    }
}
