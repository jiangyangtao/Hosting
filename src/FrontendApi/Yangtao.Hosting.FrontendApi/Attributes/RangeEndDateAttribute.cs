namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RangeEndDateAttribute : Attribute
    {
        public RangeEndDateAttribute(string rangeName)
        {
            RangeName = rangeName;
        }

        public string? RangeName { set; get; }
    }
}
