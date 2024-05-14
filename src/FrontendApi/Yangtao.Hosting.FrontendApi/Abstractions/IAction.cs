namespace Yangtao.Hosting.FrontendApi.Abstractions
{
    internal interface IAction : IHttpAction
    {
        public Type? RequestData { get; }
    }
}
