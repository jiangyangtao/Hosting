using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    internal class InputNumberControl : PrefixSuffixControlBase
    {
        public InputNumberControl(PropertyInfo property) : base(property)
        {
            ChangeStep = 1;

            var rangeAttr = property.GetCustomAttribute<RangeAttribute>();
            if (rangeAttr != null)
            {
                Min = rangeAttr.Minimum;
                Max = rangeAttr.Maximum;
            }

            var percentageRangeAttr = property.GetCustomAttribute<PercentageRangeAttribute>(false);
            if (percentageRangeAttr != null)
            {
                Min = percentageRangeAttr.Minimum;
                Max = percentageRangeAttr.Maximum;
            }

            var inputNumberAttr = property.GetCustomAttribute<InputNumberAttribute>();
            if (inputNumberAttr != null)
            {
                ChangeStep = inputNumberAttr.Step;
                Keyboard = inputNumberAttr.Keyboard;
                ShowNumberControls = inputNumberAttr.ShowNumberControls;
                Bordered = inputNumberAttr.Bordered;
            }
        }

        public bool ShowNumberControls { set; get; } = true;

        public bool Keyboard { set; get; } = true;

        public bool Bordered { set; get; } = true;

        public int ChangeStep { set; get; }

        public object? Max { set; get; }

        public object? Min { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public override ControlType ControlType => ControlType.InputNumber;
    }
}
