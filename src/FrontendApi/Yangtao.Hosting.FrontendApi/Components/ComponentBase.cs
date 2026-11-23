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
        protected ComponentBase(Type type)
        {
            ComponentName = type.Name;
            ComponentTypeFullName = type.FullName;

            var moudleAttribute = type.GetCustomAttribute<ModuleAttribute>();
            if (moudleAttribute != null)
            {
                Module = moudleAttribute.ModuleType.Name.RemoveController();
                ModuleName = StaticDocument.DocumentHandler.GetModuleSummary(moudleAttribute.ModuleType);
            }

        }

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
            if (Module.IsNullOrEmpty()) throw new InvalidOperationException($"{componentType.FullName} must specify set Module or config ModuleAttribute.");
        }
    }
}
