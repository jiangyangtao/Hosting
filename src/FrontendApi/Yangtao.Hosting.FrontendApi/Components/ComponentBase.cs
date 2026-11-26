using System.Reflection;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;

namespace Yangtao.Hosting.FrontendApi.Components
{
    /// <summary>
    /// 组件基类
    /// </summary>
    internal abstract class ComponentBase : IComponent, IModule
    {
        protected ComponentBase(Type type, IModuleType? module = null)
        {
            ComponentName = type.Name;
            ComponentTypeFullName = type.FullName;

            SetModule(module);
            if (HasModule == false)
            {
                var moduleAttr = type.GetCustomAttribute<ModuleAttribute>(false);
                SetModule(moduleAttr);
            }
        }

        private void SetModule(IModuleType? module)
        {
            if (module == null) return;
            if (HasModule) return;

            if (module.Module.NotNullAndEmpty())
            {
                Module = module.Module;
                ModuleName = module.ModuleName;
            }

            if (module.ModuleType != null)
            {
                Module = module.ModuleType.Name.RemoveController();
                ModuleName = StaticDocument.DocumentHandler.GetModuleSummary(module.ModuleType);
            }
        }

        private bool HasModule => Module.NotNullAndEmpty();

        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public string? ComponentTypeFullName { get; }

        public string? ComponentName { get; set; }

        public string? Module { set; get; }

        public string? ModuleName { set; get; }

        /// <summary>
        /// ComponentName / ComponentTypeFullName 是否全部可用
        /// <para>true 可用</para>
        /// <para>false 不可用</para>
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public bool IsValidName => ComponentName.NotNullAndEmpty() && ComponentTypeFullName.NotNullAndEmpty();

        public virtual void ValidateModule(Type componentType)
        {
            if (HasModule == false) throw new InvalidOperationException($"{componentType.FullName} must specify set Module or config ModuleAttribute.");
        }
    }
}
