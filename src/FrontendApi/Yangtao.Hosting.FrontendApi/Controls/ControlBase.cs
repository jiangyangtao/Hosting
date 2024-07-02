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
    internal abstract class ControlBase : FieldBase, IControl
    {
        protected ControlBase()
        {
        }

        protected ControlBase(PropertyInfo property, DocumentHandler documentHandler) : base(property, documentHandler)
        {
            FieldType = property.PropertyType.GetFieldType();

            var existValidation = property.GetCustomAttribute<ExistValidationAttribute>();
            if (existValidation != null) ExistValidation = new(existValidation, documentHandler);

            var requiredAttr = property.GetCustomAttribute<RequiredAttribute>();
            if (requiredAttr != null) Required = true;

            var defaultValueAttr = property.GetCustomAttribute<DefaultValueAttribute>();
            if (defaultValueAttr != null) DefaultValue = defaultValueAttr.Value;
        }

        public bool Required { protected set; get; } = false;

        public object? DefaultValue { protected set; get; }

        public ExistValidation? ExistValidation { get; }

        public abstract ControlType ControlType { get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public override FieldType FieldType { get; }
    }
}
