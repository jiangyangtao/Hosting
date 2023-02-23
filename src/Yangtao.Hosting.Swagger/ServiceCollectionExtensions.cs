using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Xml.Linq;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.Swagger.Filters;

namespace Yangtao.Hosting.Swagger
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, Action<SwaggerOptions> action)
        {
            var swaggerOptions = new SwaggerOptions();
            services.AddSwaggerGen(options =>
            {

                options.OrderActionsBy(o => o.RelativePath);     
                var referencedAssemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
                referencedAssemblies.ToList().ForEach(assembly =>
                {
                    var path = Path.Combine(AppContext.BaseDirectory, $"{assembly.Name}.xml");
                    if (File.Exists(path)) options.IncludeXmlComments(path);
                });

                var xmlFileName = swaggerOptions.ProjectXmlFileName.NotNullAndEmpty() ? swaggerOptions.ProjectXmlFileName : $"{swaggerOptions.ProjectName}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
                if (File.Exists(xmlPath)) options.IncludeXmlComments(xmlPath);

                var doc = XDocument.Load(xmlPath);
                options.OperationFilter<SwaggerOperationFilter>(doc);
                options.SchemaFilter<SwaggerSchemaFilter>();
                options.DocumentFilter<SwaggerDocumentFilter>();











                swaggerOptions.SwaggerGenOptions = options;
                action?.Invoke(swaggerOptions);

                options.SwaggerDoc(swaggerOptions.ProjectName, swaggerOptions.OpenApiInfo);

            });
            return services;
        }
    }
}