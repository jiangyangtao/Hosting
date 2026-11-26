using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Abstractions
{
    /// <summary>
    /// Http 操作接口
    /// </summary>
    internal interface IHttpAction
    {
        string? ActionApi { get; }

        HttpMethodType HttpMethodType { get; }

        ApiVersion ApiVersion { get; }

        string? ServiceName { get; }
    }

    /// <summary>
    /// Http 操作
    /// </summary>
    internal class HttpAction : IHttpAction
    {
        public HttpAction(IHttpAction httpAction)
        {
            HttpMethodType = httpAction.HttpMethodType;
            ActionApi = httpAction.ActionApi;
            ApiVersion = httpAction.ApiVersion;
            ServiceName = httpAction.ServiceName;
        }

        public HttpAction(HttpMethodType httpMethodType, string? actionApi, ApiVersion version = ApiVersion.v1, string serviceName = "")
        {
            HttpMethodType = httpMethodType;
            ActionApi = actionApi;
            ApiVersion = version;
            ServiceName = serviceName;
        }

        public string? ActionApi { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public HttpMethodType HttpMethodType { set; get; } = HttpMethodType.Get;

        [JsonConverter(typeof(StringEnumConverter))]
        public ApiVersion ApiVersion { set; get; } = ApiVersion.v1;

        public string? ServiceName { set; get; }
    }
}
