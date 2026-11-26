using Yangtao.Hosting.FrontendApi.Abstractions;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class QueryFormAttribute : Attribute, IModuleType
    {
        public Type? ModuleType { set; get; }

        public string? Module { set; get; }

        public string? ModuleName { set; get; }
    }
}
