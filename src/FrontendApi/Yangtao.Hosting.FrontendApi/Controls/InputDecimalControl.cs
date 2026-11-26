using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    internal class InputDecimalControl : PrefixSuffixControlBase
    {
        public InputDecimalControl(PropertyInfo property) : base(property)
        {
            ChangeStep = 1M;

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

            var inputNumberAttr = property.GetCustomAttribute<InputDecimalAttribute>();
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

        public decimal ChangeStep { set; get; }

        public object? Max { set; get; }

        public object? Min { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public override ControlType ControlType => ControlType.InputDecimal;
    }
}
