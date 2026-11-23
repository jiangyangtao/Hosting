using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RadioGroupAttribute : HttpActionAttributeBase
    {
        public RadioGroupAttribute(string serviceName, string apiSource, RadioGroupType radioGroupType = RadioGroupType.RadioGroupDeepBtn, HttpMethodType actionType = Enums.HttpMethodType.Get) : base(apiSource, actionType)
        {
            SourceType = SelectSourceType.API;
            RadioGroupType = radioGroupType;
            ServiceName = serviceName;
        }

        public RadioGroupAttribute(Type controllerType, string methodName, RadioGroupType radioGroupType = RadioGroupType.RadioGroupDeepBtn) : base(controllerType, methodName)
        {
            SourceType = SelectSourceType.API;
            RadioGroupType = radioGroupType;
        }

        public RadioGroupAttribute(Type enumSource, RadioGroupType radioGroupType = RadioGroupType.RadioGroupDeepBtn)
        {
            Source = enumSource;
            SourceType = SelectSourceType.Enum;
            RadioGroupType = radioGroupType;
        }


        public SelectSourceType SourceType { get; }

        public RadioGroupType RadioGroupType { set; get; }

        public string? TrueText { set; get; } = "是";

        public string? FalseText { set; get; } = "否";

        public Type? Source { get; }
    }
}
