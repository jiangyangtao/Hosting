using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    internal class SwitchControl : ControlBase, ISwitch
    {
        public SwitchControl(PropertyInfo property, DocumentHandler documentHandler) : base(property, documentHandler)
        {
            CheckedValue = true;
            UnCheckedValue = false;
            CheckedChildren = "是";
            UnCheckedChildren = "否";
        }

        public SwitchControl(SwitchAttribute switchAttribute, PropertyInfo property, DocumentHandler documentHandler) : base(property, documentHandler)
        {
            if (switchAttribute.IsEmpty == false)
            {
                CheckedValue = switchAttribute.CheckedValue;
                UnCheckedValue = switchAttribute.UnCheckedValue;
                CheckedChildren = switchAttribute.CheckedChildren;
                UnCheckedChildren = switchAttribute.UnCheckedChildren;
            }

            if (switchAttribute.IsEmpty)
            {
                if (switchAttribute.IsEmpty && FieldType != FieldType.Enum) throw new Exception("Cannot infer the properties of the Switch control");

                var valueOptions = property.PropertyType.GetValueOptions(documentHandler);
                if (valueOptions.Count() != 2) throw new Exception($"The properties of the switch control cannot be inferred from the field collection of the {property.PropertyType.Name}.");

                var options = valueOptions.ToArray();
                CheckedValue = options[0].Value;
                CheckedChildren = options[0].Text;

                UnCheckedValue = options[1].Value;
                UnCheckedChildren = options[1].Text;
            }
        }

        public object? CheckedValue { set; get; }

        public object? UnCheckedValue { set; get; }

        public object? CheckedChildren { set; get; }

        public object? UnCheckedChildren { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public override ControlType ControlType => ControlType.Switch;
    }
}
