using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.FrontendApi
{
    internal class FrontendComponentBuilder
    {
        private const string ProjectType = "project";

        private readonly FrontendModules frontendModules;

        public FrontendComponentBuilder()
        {
            var assemblies = DependencyContext.Default.CompileLibraries.Where(a => a.Type == ProjectType).Select(a => Assembly.Load(a.Name)).ToArray();
            frontendModules = new FrontendModules(assemblies);
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
