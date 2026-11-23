using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    internal class InputControl : PrefixSuffixControlBase
    {
        public InputControl(PropertyInfo property) : base(property)
        {
            var maxLenthAttr = property.GetCustomAttribute<MaxLengthAttribute>();
            if (maxLenthAttr != null) MaxLength = maxLenthAttr.Length;

            var inputAttr = property.GetCustomAttribute<InputAttribute>();
            if (inputAttr != null)
            {
                AllowClear = inputAttr.AllowClear;
                ShowCount = inputAttr.ShowCount;
                Bordered = inputAttr.Bordered;
            }
        }

        public int? MaxLength { set; get; }

        public bool ShowCount { set; get; } = true;

        public bool AllowClear { set; get; } = true;

        public bool Bordered { set; get; } = true;

        [JsonConverter(typeof(StringEnumConverter))]
        public override ControlType ControlType => ControlType.Input;
    }
}
