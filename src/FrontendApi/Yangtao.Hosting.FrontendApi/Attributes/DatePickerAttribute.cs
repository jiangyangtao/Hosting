using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DatePickerAttribute : Attribute
    {
        public DatePickerAttribute(TimeUnit timeUnit = TimeUnit.Day, bool showToday = true)
        {
            ShowToday = showToday;
            TimeUnit = timeUnit;
        }

        public bool AllowClear { set; get; } = true;

        public bool ShowToday { set; get; }

        public bool Bordered { set; get; } = true;

        public TimeUnit TimeUnit { set; get; }
    }
}
