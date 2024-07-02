using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    internal class RangePickerControl : ControlBase
    {
        public RangePickerControl(RangeControlInfo rangeControlInfo, RangeEndDateControlInfo endDateControlInfo, DocumentHandler documentHandler) : base(rangeControlInfo.Property, documentHandler)
        {
            BeginDate = rangeControlInfo.Property.Name;
            RangeName = rangeControlInfo.RangeName;
            AllowClear = rangeControlInfo.RangePickerAttr.AllowClear;
            Bordered = rangeControlInfo.RangePickerAttr.Bordered;
            Separator = rangeControlInfo.RangePickerAttr.Separator;
            ShowTime = rangeControlInfo.RangePickerAttr.ShowTime;

            EndDate = endDateControlInfo.Property.Name;
        }

        public string? BeginDate { set; get; }

        public string? EndDate { set; get; }

        public string? RangeName { set; get; }

        public bool AllowClear { set; get; } = true;

        public bool Bordered { set; get; } = true;

        public string? Separator { set; get; }

        public bool ShowTime { set; get; } = false;

        [JsonConverter(typeof(StringEnumConverter))]
        public override ControlType ControlType => ControlType.RangePicker;
    }
}
