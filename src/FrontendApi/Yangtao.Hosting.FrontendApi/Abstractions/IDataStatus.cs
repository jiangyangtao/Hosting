
namespace Yangtao.Hosting.FrontendApi.Abstractions
{
    internal interface IDataStatus
    {
        public Type? RequestData { get; }

        public IHttpAction EnableHttpAction { get; }

        public IHttpAction DisableHttpAction { get; }
    }
}
