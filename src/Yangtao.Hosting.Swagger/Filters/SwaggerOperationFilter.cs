using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Xml.Linq;
using System.Xml.XPath;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.Swagger.Filters
{
    internal class SwaggerOperationFilter : IOperationFilter
    {
        private readonly XDocument xmlDoc;

        public SwaggerOperationFilter(XDocument doc)
          => xmlDoc = doc;

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {

            if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor descriptor)
            {
                #region 处理无返回数据

                foreach (var response in operation.Responses)
                {
                    if (response.Key == "200" && response.Value.Content.Count == 0)
                    {
                        response.Value.Description = string.Empty;
                    }
                }
                #endregion

                #region 处理中文分组

                var controllerName = $"T:{descriptor.MethodInfo.DeclaringType.FullName}";
                var description = xmlDoc.XPathEvaluate($"normalize-space(//member[@name = '{controllerName}']/summary/text())") as string;
                if (description.NotNullAndEmpty())
                {
                    operation.Tags[0].Name = description;
                }
                #endregion

                #region 处理备注

                if (operation.Description.NotNullAndEmpty())
                {
                    operation.Description = operation.Description.Replace("\r\n", " <br>\n ");
                }

                #endregion
            }
        }
    }
}
