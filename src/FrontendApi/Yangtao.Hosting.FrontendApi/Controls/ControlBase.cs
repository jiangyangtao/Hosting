using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Components;
using Yangtao.Hosting.FrontendApi.Enums;
using Yangtao.Hosting.FrontendApi.Fields;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    /// <summary>
    /// 控件基类
    /// </summary>
    internal abstract class ControlBase : FieldBase, IControl
    {
        protected ControlBase()
        {
        }

        protected ControlBase(PropertyInfo property) : base(property)
        {
            FieldType = property.GetFieldType();

            var existValidation = property.GetCustomAttribute<ExistValidationAttribute>();
            if (existValidation != null) ExistValidation = new(existValidation);

            var requiredAttr = property.GetCustomAttribute<RequiredAttribute>();
            if (requiredAttr != null) Required = true;

            var defaultValueAttr = property.GetCustomAttribute<DefaultValueAttribute>();
            if (defaultValueAttr != null) DefaultValue = defaultValueAttr.Value;
        }

        public bool Required { protected set; get; } = false;

        public object? DefaultValue { protected set; get; }

        public ExistValidation? ExistValidation { get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public abstract ControlType ControlType { get; }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public override FieldType FieldType { get; }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public override bool IsKey { get => base.IsKey; protected set => base.IsKey = value; }
    }
}
