using System.Reflection;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;

namespace Yangtao.Hosting.FrontendApi.Components
{
    internal abstract class ComponentBase : IComponent, IModule
    {
        protected ComponentBase(Type type, XmlDocumentHandler xmlHandler)
        {
            ComponentName = type.Name;

            var moudleAttribute = type.GetCustomAttribute<ModuleAttribute>();
            if (moudleAttribute != null)
            {
                Module = moudleAttribute.ModuleType.Name.RemoveController();
                ModuleName = xmlHandler.GetModuleSummary(moudleAttribute.ModuleType);
            }
        }

        public string? ComponentName { get; set; }

        public string? Module { set; get; }

        public string? ModuleName { set; get; }

        internal bool IsEmptyModule => Module.IsNullOrEmpty();
    }
}
