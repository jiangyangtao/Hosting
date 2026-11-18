using Yangtao.Hosting.FrontendApi.Abstractions;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class BooleanFieldAttribute : Attribute, IBoolean
    {
        public BooleanFieldAttribute()
        {
        }

        public string? TrueText { set; get; } = "是";

        public string? FalseText { set; get; } = "否";

        public bool TagMode { set; get; } = false;
    }
}
