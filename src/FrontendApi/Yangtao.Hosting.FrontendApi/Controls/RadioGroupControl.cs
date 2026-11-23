using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Attributes;
using Yangtao.Hosting.FrontendApi.Enums;
using Yangtao.Hosting.Mvc.FormatResult;

namespace Yangtao.Hosting.FrontendApi.Controls
{
    internal class RadioGroupControl : ControlBase, IHttpAction
    {
        public RadioGroupControl(RadioGroupAttribute radioGroupAttribute, PropertyInfo property, FieldType fieldType) : base(property)
        {
            RadioGroupType = radioGroupAttribute.RadioGroupType;
            ActionApi = radioGroupAttribute.ActionApi;
            ApiVersion = radioGroupAttribute.ApiVersion;
            SourceType = radioGroupAttribute.SourceType;
            HttpMethodType = radioGroupAttribute.HttpMethodType;
            ServiceName = StaticDocument.DocumentHandler.GetServiceName(radioGroupAttribute.ServiceName);


            if (fieldType == FieldType.Boolean)
            {
                EnumOptions = new TextValueOption[] { new("true", radioGroupAttribute.TrueText), new("false", radioGroupAttribute.FalseText) };
            }

            if (radioGroupAttribute.SourceType == SelectSourceType.Enum)
            {
                var type = radioGroupAttribute.Source ?? property.PropertyType;
                EnumOptions = type.GetValueOptions();
            }
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public RadioGroupType RadioGroupType { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public override ControlType ControlType => ControlType.RadioGroup;

        [JsonConverter(typeof(StringEnumConverter))]
        public SelectSourceType SourceType { set; get; }

        public IEnumerable<TextValueOption> EnumOptions { set; get; } = TextValueOption.Empty();

        public string? ActionApi { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public HttpMethodType HttpMethodType { set; get; } = HttpMethodType.Get;

        [JsonConverter(typeof(StringEnumConverter))]
        public ApiVersion ApiVersion { set; get; } = ApiVersion.v1;

        public string? ServiceName { set; get; }
    }
}
