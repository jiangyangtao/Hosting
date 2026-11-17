using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Enums;
using Yangtao.Hosting.FrontendApi.Fields;

namespace Yangtao.Hosting.FrontendApi
{
    internal abstract class ActionBase : IAction
    {
        [JsonIgnore]
        public virtual Type? RequestData { protected set; get; }

        public IEnumerable<ParamField> RequestParams { protected set; get; }

        public string? ActionApi { protected set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public HttpMethodType? HttpMethodType { protected set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ApiVersion ApiVersion { protected set; get; } = ApiVersion.v1;

        public string? ServiceName { protected set; get; }
    }
}
