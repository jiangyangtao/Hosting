using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DeleteActionAttribute : HttpActionAttributeBase, IAction
    {
        public DeleteActionAttribute(Type? requestData, string? actionApi) : base(actionApi, Enums.HttpMethodType.Delete)
        {
            RequestData = requestData;
        }

        public DeleteActionAttribute(Type? requestData, Type controller, string methodName) : base(controller, methodName)
        {
            RequestData = requestData;
        }

        public Type? RequestData { get; }
    }
}
