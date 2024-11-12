using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SelectAttribute : HttpActionAttributeBase
    {
        public SelectAttribute()
        {
            SourceType = SelectSourceType.Enum;
        }

        public SelectAttribute(string apiSource, HttpMethodType actionType = Enums.HttpMethodType.Get, SelectMode mode = SelectMode.Tags) : base(apiSource, actionType)
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
            Source = enumSource;
            SourceType = SelectSourceType.Enum;
            SelectMode = mode;
        }

        public SelectMode SelectMode { get; } = SelectMode.Tags;

        public SelectSourceType SourceType { get; }

        public bool Bordered { set; get; } = true;

        public bool AllowClear { set; get; } = true;

        public bool ShowSearch { set; get; } = true;

        public Type? Source { get; }
    }
}
