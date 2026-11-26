using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    public abstract class HttpActionAttributeBase : Attribute, IHttpAction
    {

        protected HttpActionAttributeBase()
        {
        }

        protected HttpActionAttributeBase(Type controllerType, string methodName)
        {
            controllerType.ValidateControllerType();

            var method = controllerType.GetMethod(methodName) ?? throw new ArgumentException($"{methodName} does not exist in the {controllerType.FullName}");
            var action = method.GetHttpAction();

            ActionApi = action.ActionApi;
            ApiVersion = action.ApiVersion;
            HttpMethodType = action.HttpMethodType;
        }

        protected HttpActionAttributeBase(string? actionApi, HttpMethodType httpActionType)
        {
            ActionApi = actionApi;
            HttpMethodType = httpActionType;
        }

        public string? ActionApi { set; get; }

        public virtual HttpMethodType HttpMethodType { set; get; } = HttpMethodType.Get;

        public ApiVersion ApiVersion { set; get; } = ApiVersion.v1;

        public string? ServiceName { set; get; }

        /// <summary>
        /// 是否可用
        /// <para>true 可用</para>
        /// <para>false 不可用</para>
        /// </summary>
        public bool IsValid => ActionApi.NotNullAndEmpty();
    }

    /// <summary>
    /// http 操作
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class HttpActionAttribute : HttpActionAttributeBase
    {
        public HttpActionAttribute(Type controllerType, string methodName) : base(controllerType, methodName)
        {

        }

        public HttpActionAttribute(string? actionApi, HttpMethodType httpMethodType) : base(actionApi, httpMethodType)
        {

        }
    }
}
