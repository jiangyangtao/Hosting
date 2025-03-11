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

        public RadioGroupAttribute(Type controller, string methodName, RadioGroupType radioGroupType = RadioGroupType.RadioGroupDeepBtn) : base(controller, methodName)
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


        public Type? Source { get; }
    }
}
