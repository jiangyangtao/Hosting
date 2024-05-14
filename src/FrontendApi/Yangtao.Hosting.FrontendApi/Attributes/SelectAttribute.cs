using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SelectAttribute : HttpActionAttributeBase
    {

        public SelectAttribute(string apiSource, HttpMethodType actionType = HttpMethodType.Get, SelectMode mode = SelectMode.Tags) : base(apiSource, actionType)
        {
            SourceType = SelectSourceType.API;
            SelectMode = mode;
        }

        public SelectAttribute(Type controller, string methodName, SelectMode mode = SelectMode.Tags) : base(controller, methodName)
        {
            SourceType = SelectSourceType.API;
            SelectMode = mode;
        }

        public SelectAttribute(Type enumSource, SelectMode mode = SelectMode.Tags)
        {
            EnumSource = enumSource;
            SourceType = SelectSourceType.Enum;
            SelectMode = mode;
        }

        public SelectMode SelectMode { get; }

        public SelectSourceType SourceType { get; }

        public bool Bordered { set; get; }

        public bool AllowClear { set; get; }

        public bool ShowSearch { set; get; }

        public Type? EnumSource { get; }
    }
}
