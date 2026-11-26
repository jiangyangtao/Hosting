using Yangtao.Hosting.FrontendApi.Abstractions;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    /// <summary>
    /// 模块
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ModuleAttribute : Attribute, IModuleType
    {
        public ModuleAttribute() {}

        public ModuleAttribute(Type module)
        {
            ModuleType = module;
        }

        public Type ModuleType { set; get; }

        public string? Module { set; get; }

        public string? ModuleName { set; get; }
    }
}
