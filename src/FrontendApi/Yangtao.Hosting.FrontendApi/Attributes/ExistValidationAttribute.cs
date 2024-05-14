using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ExistValidationAttribute : HttpActionAttributeBase, IAction
    {
        public ExistValidationAttribute(Type requestData, string? actionApi, HttpMethodType httpActionType) : base(actionApi, httpActionType)
        {
            RequestData = requestData;
        }

        public ExistValidationAttribute(Type requestData, Type controller, string methodName) : base(controller, methodName)
        {
            RequestData = requestData;
        }

        public Type? RequestData { set; get; }
    }
}
