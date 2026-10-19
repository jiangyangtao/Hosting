namespace Yangtao.Hosting.FrontendApi.Abstractions
{
    /// <summary>
    /// 操作接口
    /// </summary>
    internal interface IAction : IHttpAction
    {
        Type? RequestData { get; }
    }
}
