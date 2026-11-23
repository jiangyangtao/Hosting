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
        public SwitchControl(PropertyInfo property) : base(property)
        {
            InitBooleanDefault();
        }

        public SwitchControl(SwitchAttribute switchAttribute, PropertyInfo property) : base(property)
        {
            DefaultValue = switchAttribute.DefaultValue;

            CheckedValue = switchAttribute.CheckedValue;
            UnCheckedValue = switchAttribute.UnCheckedValue;
            CheckedChildren = switchAttribute.CheckedChildren;
            UnCheckedChildren = switchAttribute.UnCheckedChildren;

            if (switchAttribute.IsValid == false)
            {
                if (FieldType == FieldType.Boolean) InitBooleanDefault();
                if (FieldType == FieldType.Enum)
                {
                    var valueOptions = property.PropertyType.GetValueOptions();
                    if (valueOptions.Count() != 2) throw new InvalidOperationException($"Properties of the switch control cannot be inferred from the field collection of the {property.PropertyType.FullName}.");

                    var options = valueOptions.ToArray();
                    CheckedValue = options[0].Value;
                    CheckedChildren = options[0].Text;

                    UnCheckedValue = options[1].Value;
                    UnCheckedChildren = options[1].Text;
                }
            }
        }

        private void InitBooleanDefault()
        {
            CheckedValue = true;
            UnCheckedValue = false;
            CheckedChildren = "是";
            UnCheckedChildren = "否";
        }

        public object? CheckedValue { set; get; }

        public object? UnCheckedValue { set; get; }

        public object? CheckedChildren { set; get; }

        public object? UnCheckedChildren { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public override ControlType ControlType => ControlType.Switch;
    }
}
