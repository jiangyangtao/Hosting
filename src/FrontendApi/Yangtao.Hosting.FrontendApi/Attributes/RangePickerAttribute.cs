using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RangePickerAttribute : Attribute
    {
        public RangePickerAttribute(string rangeName)
        {
            RangeName = rangeName;
        }

        public string? RangeName { set; get; }

        public string? Separator { set; get; }

        public bool ShowTime { set; get; } = false;

        public bool AllowClear { set; get; } = true;

        public bool Bordered { set; get; } = true;
    }
}
