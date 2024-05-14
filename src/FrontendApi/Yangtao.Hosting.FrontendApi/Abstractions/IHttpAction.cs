using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Abstractions
{
    internal interface IHttpAction
    {
        public string? ActionApi { get; }

        public HttpMethodType? HttpActionType { get; }

        public HttpVersion HttpVersion { get; }

        public string? ServiceName { get; }
    }

    internal class HttpAction : IHttpAction
    {
        public HttpAction(HttpMethodType httpActionType, string? actionApi)
        {
            HttpActionType = httpActionType;
            ActionApi = actionApi;
        }

        public string? ActionApi { set; get; }

        public HttpMethodType? HttpActionType { set; get; }

        public HttpVersion HttpVersion { set; get; } = HttpVersion.v1;

        public string? ServiceName { set; get; }
    }
}
