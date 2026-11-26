using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DataStatusAttribute : Attribute
    {
        public DataStatusAttribute(
            Type requestData,
            Type controllerApi,
            string? enableAction,
            string? disableAction,
            ApiVersion enableActionVersion = ApiVersion.v1,
            ApiVersion disableActionVersion = ApiVersion.v1,
            string serviceName = "")
        {
            RequestData = requestData;

            controllerApi.ValidateControllerType();

            var enableMethod = controllerApi.GetMethod(enableAction) ?? throw new ArgumentException($"{enableAction} not exist {controllerApi.FullName}");
            EnableHttpAction = enableMethod.GetHttpAction(enableActionVersion, serviceName);

            var disableMethod = controllerApi.GetMethod(disableAction) ?? throw new ArgumentException($"{disableAction} not exist {controllerApi.FullName}");
            DisableHttpAction = disableMethod.GetHttpAction(disableActionVersion, serviceName);
        }

        public Type? RequestData { get; }

        internal IHttpAction EnableHttpAction { get; }

        internal IHttpAction DisableHttpAction { get; }

        public string TrueText { set; get; }

        public string FalseText { set; get; }
    }
}
