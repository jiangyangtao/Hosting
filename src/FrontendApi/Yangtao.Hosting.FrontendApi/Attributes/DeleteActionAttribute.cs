using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DeleteActionAttribute : HttpActionAttributeBase, IAction
    {
        public DeleteActionAttribute(Type? requestData, string? actionApi) : base(actionApi, Enums.HttpMethodType.Delete)
        {
            RequestData = requestData;
        }

        public DeleteActionAttribute(Type? requestData, Type controllerType, string methodName) : base(controllerType, methodName)
        {
            RequestData = requestData;
        }

        public Type? RequestData { get; }

        public override HttpMethodType HttpMethodType { set; get; } = HttpMethodType.Delete;
    }
}
