using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DataStatusAttribute : Attribute
    {
        public DataStatusAttribute(
            Type requestData,
            Type controller,
            string? enableAction,
            string? disableAction,
            HttpVersion enableActionVersion = HttpVersion.v1,
            HttpVersion disableActionVersion = HttpVersion.v1,
            string serviceName = "")
        {
            RequestData = requestData;

            var enableMethod = controller.GetMethod(enableAction) ?? throw new ArgumentException($"${enableAction} not exist {controller.Name}");
            var enableHttpAction = enableMethod.GetHttpAction(enableActionVersion, serviceName);
            EnableHttpAction = enableHttpAction;

            var disableMethod = controller.GetMethod(disableAction) ?? throw new ArgumentException($"${disableAction} not exist {controller.Name}");
            var disableHttpAction = disableMethod.GetHttpAction(disableActionVersion, serviceName);
            DisableHttpAction = disableHttpAction;
        }

        public Type? RequestData { set; get; }

        internal IHttpAction EnableHttpAction { get; }

        internal IHttpAction DisableHttpAction { get; }
    }
}
