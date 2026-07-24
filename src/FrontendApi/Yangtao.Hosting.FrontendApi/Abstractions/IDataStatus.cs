
namespace Yangtao.Hosting.FrontendApi.Abstractions
{
    /// <summary>
    /// 数据状态接口
    /// </summary>
    internal interface IDataStatus
    {
        public Type? RequestData { get; }

        public IHttpAction EnableHttpAction { get; }

        public IHttpAction DisableHttpAction { get; }
    }
}
