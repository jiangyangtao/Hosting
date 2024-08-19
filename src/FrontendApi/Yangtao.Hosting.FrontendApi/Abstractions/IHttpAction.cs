using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Abstractions
{
    internal interface IHttpAction
    {
        public string? ActionApi { get; }

        public HttpMethodType? HttpActionType { get; }

        public ApiVersion HttpVersion { get; }

        public string? ServiceName { get; }
    }

    internal class HttpAction : IHttpAction
    {
        public HttpAction(HttpMethodType httpActionType, string? actionApi, ApiVersion version = ApiVersion.v1, string serviceName = "")
        {
            HttpActionType = httpActionType;
            ActionApi = actionApi;
            HttpVersion = version;
            ServiceName = serviceName;
        }

        public string? ActionApi { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public HttpMethodType? HttpActionType { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ApiVersion HttpVersion { set; get; } = ApiVersion.v1;

        public string? ServiceName { set; get; }
    }
}
