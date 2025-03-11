using Yangtao.Hosting.FrontendApi.Abstractions;
using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi
{
    public class DictionaryOptionsConfig : IHttpAction
    {
        public string? ActionApi { set; get; }

        public HttpMethodType? HttpMethodType { set; get; } = Enums.HttpMethodType.Get;

        public ApiVersion ApiVersion { set; get; } = ApiVersion.v1;

        public string? ServiceName { set; get; }
    }
}
