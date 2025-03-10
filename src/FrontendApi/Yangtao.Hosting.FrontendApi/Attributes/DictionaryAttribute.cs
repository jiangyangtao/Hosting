using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DictionaryAttribute : HttpActionAttributeBase
    {
        public string? Module { set; get; }

        public SelectMode SelectMode { set; get; } = SelectMode.Tags;

        public SelectSourceType SourceType { get; } = SelectSourceType.API;

        public OptionsType OptionsType { get; } = OptionsType.Select;

        public bool Bordered { set; get; } = true;

        public bool AllowClear { set; get; } = true;

        public bool ShowSearch { set; get; } = true;
    }
}
