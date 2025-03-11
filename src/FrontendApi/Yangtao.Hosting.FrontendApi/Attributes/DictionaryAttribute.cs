using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DictionaryAttribute : HttpActionAttributeBase
    {
        public string? Module { set; get; }

        public SelectMode SelectMode { set; get; } = SelectMode.Tags;

        public SelectSourceType SourceType { get; } = SelectSourceType.API;

        public DictionaryOptionsType OptionsType { get; } = DictionaryOptionsType.Select;

        public bool Bordered { set; get; } = true;

        public bool AllowClear { set; get; } = true;

        public bool ShowSearch { set; get; } = true;


        public bool IsRadioGroup
        {
            get
            {
                if (OptionsType == DictionaryOptionsType.RadioGroup) return true;
                if (OptionsType == DictionaryOptionsType.RadioGroupBtn) return true;
                if (OptionsType == DictionaryOptionsType.RadioGroupDeepBtn) return true;
                if (OptionsType == DictionaryOptionsType.RadioGroupVertical) return true;

                return false;
            }
        }
    }
}
