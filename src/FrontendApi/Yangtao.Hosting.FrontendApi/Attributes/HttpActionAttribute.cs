using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    public abstract class HttpActionAttributeBase : Attribute, IHttpAction
    {
        protected HttpActionAttributeBase()
        {
        }

        protected HttpActionAttributeBase(Type controller, string methodName)
        {
            var method = controller.GetMethod(methodName) ?? throw new ArgumentException($"${methodName} not exist {controller.Name}");
            var action = method.GetHttpAction();

            ActionApi = action.ActionApi;
            HttpActionType = action.HttpActionType;
        }

        protected HttpActionAttributeBase(string? actionApi, HttpMethodType httpActionType)
        {
            ActionApi = actionApi;
            HttpActionType = httpActionType;
        }

        public string? ActionApi { set; get; }

        public HttpMethodType? HttpActionType { set; get; }

        public ApiVersion ApiVersion { set; get; } = ApiVersion.v1;

        public string? ServiceName { set; get; }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class HttpActionAttribute : HttpActionAttributeBase
    {
        public HttpActionAttribute(Type controller, string methodName) : base(controller, methodName)
        {
            
        }

        public HttpActionAttribute(string? actionApi, HttpMethodType httpActionType) : base(actionApi, httpActionType)
        {

        }
    }
}
