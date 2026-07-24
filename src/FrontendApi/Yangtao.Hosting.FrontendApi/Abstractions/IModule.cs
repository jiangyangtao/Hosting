namespace Yangtao.Hosting.FrontendApi.Abstractions
{
    /// <summary>
    /// 模块接口
    /// </summary>
    internal interface IModule
    {
        public string? Module {  get; }

        public string? ModuleName {  get; }
    }
}
