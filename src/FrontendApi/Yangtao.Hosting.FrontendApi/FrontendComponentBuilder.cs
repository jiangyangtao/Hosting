using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.FrontendApi
{
    internal class FrontendComponentBuilder
    {
        private const string ProjectType = "project";

        private readonly FrontendModules frontendModules;

        public FrontendComponentBuilder(FrontendApiConfigurationOptions options, ILogger logger)
        {
            var assemblies = Array.Empty<Assembly>();
            assemblies = DependencyContext.Default?.CompileLibraries.Where(a => a.Type == ProjectType).Select(a => Assembly.Load(a.Name)).ToArray();
            if (assemblies.IsNullOrEmpty()) assemblies = AppDomain.CurrentDomain.GetAssemblies();

            frontendModules = new FrontendModules(assemblies, options, logger);
        }

        private string JsonData { set; get; } = string.Empty;

        public string BuildJson()
        {
            if (JsonData.IsNullOrEmpty()) JsonData = frontendModules.ToJson();

            return JsonData;
        }

        public FrontendModules FrontendModules => frontendModules;
    }
}
