using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IndependentEditAttribute : HttpActionAttributeBase, IAction
    {
        public IndependentEditAttribute(Type requestData, string? actionApi, HttpMethodType httpActionType) : base(actionApi, httpActionType)
        {
            RequestData = requestData;
        }

        public IndependentEditAttribute(Type requestData, Type controller, string methodName) : base(controller, methodName)
        {
            RequestData = requestData;
        }

        public Type? RequestData { set; get; }
    }
}
