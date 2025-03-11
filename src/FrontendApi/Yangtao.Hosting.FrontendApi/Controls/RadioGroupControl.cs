using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;
using Yangtao.Hosting.Mvc.FormatResult;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    internal class RadioGroupControl : ControlBase
    {
        public RadioGroupControl(RadioGroupAttribute radioGroupAttribute, PropertyInfo property, FieldType fieldType, DocumentHandler documentHandler) : base(property, documentHandler)
        {
            RadioGroupType = radioGroupAttribute.RadioGroupType;
            ActionApi = radioGroupAttribute.ActionApi;
            SourceType = radioGroupAttribute.SourceType;
            HttpMethodType = radioGroupAttribute.HttpMethodType;
            ServiceName = documentHandler.GetServiceName(radioGroupAttribute.ServiceName);


            if (fieldType == FieldType.Boolean)
            {
                EnumOptions = new TextValueOption[] { new("true", "否"), new("false", "是") };
            }

            if (radioGroupAttribute.SourceType == SelectSourceType.Enum)
            {
                var type = radioGroupAttribute.Source ?? property.PropertyType;
                EnumOptions = type.GetValueOptions(documentHandler);
            }
        }

        public RadioGroupControl(DictionaryOptionsAttribute dictionaryAttribute, PropertyInfo property, DocumentHandler documentHandler) : base(property, documentHandler)
        {
            if (dictionaryAttribute.OptionsType == DictionaryOptionsType.RadioGroup) RadioGroupType = RadioGroupType.RadioGroup;
            if (dictionaryAttribute.OptionsType == DictionaryOptionsType.RadioGroupBtn) RadioGroupType = RadioGroupType.RadioGroupBtn;
            if (dictionaryAttribute.OptionsType == DictionaryOptionsType.RadioGroupDeepBtn) RadioGroupType = RadioGroupType.RadioGroupDeepBtn;
            if (dictionaryAttribute.OptionsType == DictionaryOptionsType.RadioGroupVertical) RadioGroupType = RadioGroupType.RadioGroupVertical;

            SourceType = dictionaryAttribute.SourceType;

            if (dictionaryAttribute.ActionApi.NotNullAndEmpty() && dictionaryAttribute.ServiceName.NotNullAndEmpty())
            {
                ActionApi = dictionaryAttribute.ActionApi;
                HttpMethodType = dictionaryAttribute.HttpMethodType;
                ApiVersion = dictionaryAttribute.ApiVersion;
                ServiceName = documentHandler.GetServiceName(dictionaryAttribute.ServiceName);
            }

            if (documentHandler.DictionaryConfig != null && dictionaryAttribute.ActionApi.IsNullOrEmpty() && dictionaryAttribute.ServiceName.IsNullOrEmpty())
            {
                ActionApi = documentHandler.DictionaryConfig.ActionApi;
                ApiVersion = documentHandler.DictionaryConfig.ApiVersion;
                HttpMethodType = documentHandler.DictionaryConfig.HttpMethodType;
                ServiceName = documentHandler.DictionaryConfig.ServiceName;
            }
        }

        public RadioGroupType RadioGroupType { set; get; }

        public override ControlType ControlType => ControlType.RadioGroup;

        [JsonConverter(typeof(StringEnumConverter))]
        public SelectSourceType SourceType { set; get; }

        public IEnumerable<TextValueOption> EnumOptions { set; get; } = TextValueOption.Empty();

        public string? ActionApi { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public HttpMethodType? HttpMethodType { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ApiVersion ApiVersion { set; get; } = ApiVersion.v1;

        public string? ServiceName { set; get; }
    }
}
