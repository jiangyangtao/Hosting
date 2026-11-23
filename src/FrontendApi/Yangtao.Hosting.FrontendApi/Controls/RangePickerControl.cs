using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    internal class RangePickerControl : ControlBase
    {
        public RangePickerControl(RangePickerAttribute rangePickerAttribute, PropertyInfo property) : base(property)
        {
            var isValidPropertyType = IsValidPropertyType(property.PropertyType);
            if (isValidPropertyType == false) throw new InvalidOperationException("RangePickerAttribute only supports DateTime[] or DateTimeOffset[] or List<DateTime> or List<DateTimeOffset> or IEnumerable<DateTime> or IEnumerable<DateTimeOffset>.");

            RangeName = rangePickerAttribute.RangeName;
            AllowClear = rangePickerAttribute.AllowClear;
            Bordered = rangePickerAttribute.Bordered;
            ShowTime = rangePickerAttribute.ShowTime;
        }

        public string? RangeName { set; get; }

        public bool AllowClear { set; get; } = true;

        public bool Bordered { set; get; } = true;

        public bool ShowTime { set; get; } = false;

        [JsonConverter(typeof(StringEnumConverter))]
        public override ControlType ControlType => ControlType.RangePicker;

        private static bool IsValidPropertyType(Type propertyType)
        {
            return propertyType == StaticKeys.DateTimeArrayType ||
                propertyType == StaticKeys.DateTimeOffsetArrayType ||
                propertyType == StaticKeys.DateTimeListType ||
                propertyType == StaticKeys.DateTimeOffsetListType ||
                propertyType == StaticKeys.DateTimeEnumerableType ||
                propertyType == StaticKeys.DateTimeOffsetEnumerableType;
        }
    }
}
