using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    internal class DatePickerControl : ControlBase, IFieldGroup
    {
        public DatePickerControl(PropertyInfo property, XmlDocumentHandler xmlHandler) : base(property, xmlHandler)
        {
            var datePickerAttr = property.GetCustomAttribute<DatePickerAttribute>();
            if (datePickerAttr != null)
            {
                AllowClear = datePickerAttr.AllowClear; ;
                ShowToday = datePickerAttr.ShowToday; ;
                TimeUnit = datePickerAttr.TimeUnit;
                Bordered = datePickerAttr.Bordered;
            }
        }

        public bool AllowClear { set; get; } = true;

        public bool ShowToday { set; get; } = true;

        public bool Bordered { set; get; } = true;

        [JsonConverter(typeof(StringEnumConverter))]
        public TimeUnit TimeUnit { set; get; } = TimeUnit.Day;

        [JsonConverter(typeof(StringEnumConverter))]
        public override ControlType ControlType => ControlType.DatePicker;
    }
}
