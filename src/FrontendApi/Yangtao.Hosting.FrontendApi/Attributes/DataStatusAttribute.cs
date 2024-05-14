using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DataStatusAttribute : Attribute, IDataStatus
    {
        public DataStatusAttribute(Type requestData, string? enableAction, string? disableAction)
        {
            RequestData = requestData;
            EnableAction = enableAction;
            DisableAction = disableAction;
        }

        public Type? RequestData { set; get; }

        public string? EnableAction { set; get; }

        public string? DisableAction { set; get; }

        public HttpMethodType HttpActionType { set; get; } = HttpMethodType.Patch;
    }
}
