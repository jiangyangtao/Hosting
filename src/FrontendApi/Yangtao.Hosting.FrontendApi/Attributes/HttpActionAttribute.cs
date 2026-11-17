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
            var method = controller.GetMethod(methodName) ?? throw new ArgumentException($"${methodName} does not exist in the {controller.Name}");
            var action = method.GetHttpAction();

            ActionApi = action.ActionApi;
            HttpMethodType = action.HttpMethodType;
        }

        protected HttpActionAttributeBase(string? actionApi, HttpMethodType httpActionType)
        {
            ActionApi = actionApi;
            HttpMethodType = httpActionType;
        }

        public string? ActionApi { set; get; }

        public HttpMethodType? HttpMethodType { set; get; }

        public ApiVersion ApiVersion { set; get; } = ApiVersion.v1;

        public string? ServiceName { set; get; }
    }

    /// <summary>
    /// http 操作
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class HttpActionAttribute : HttpActionAttributeBase
    {
        public HttpActionAttribute(Type controller, string methodName) : base(controller, methodName)
        {

        }

        public HttpActionAttribute(string? actionApi, HttpMethodType httpMethodType) : base(actionApi, httpMethodType)
        {

        }
    }
}
