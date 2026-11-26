using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Enums;
using Yangtao.Hosting.FrontendApi.Fields;

namespace Yangtao.Hosting.FrontendApi
{
    internal abstract class ActionBase : IAction
    {
        protected ActionBase(IAction action)
        {
            ActionApi = action.ActionApi;
            HttpMethodType = action.HttpMethodType;
            ApiVersion = action.ApiVersion;
            ServiceName = StaticDocument.DocumentHandler.GetServiceName(action.ServiceName);
            if (action.RequestData != null) RequestParams = action.RequestData.BuildParamFields();
        }

        [JsonIgnore]
        public virtual Type? RequestData { protected set; get; }

        public IEnumerable<ParamField> RequestParams { protected set; get; } = Array.Empty<ParamField>();

        public string? ActionApi { protected set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public virtual HttpMethodType HttpMethodType { protected set; get; } = HttpMethodType.Get;

        [JsonConverter(typeof(StringEnumConverter))]
        public ApiVersion ApiVersion { protected set; get; } = ApiVersion.v1;

        public string? ServiceName { protected set; get; }
    }
}
