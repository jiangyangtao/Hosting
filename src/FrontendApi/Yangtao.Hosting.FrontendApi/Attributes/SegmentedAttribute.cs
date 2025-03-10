using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SegmentedAttribute : HttpActionAttributeBase
    {
        public bool Block { set; get; } = false;

        public SelectSourceType SourceType { get; }
    }
}
