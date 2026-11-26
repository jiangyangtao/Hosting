namespace Yangtao.Hosting.FrontendApi.Abstractions
{
    /// <summary>
    /// 模块接口
    /// </summary>
    internal interface IModule
    {
        string? Module { get; }

        string? ModuleName { get; }
    }

    /// <summary>
    /// 模块接口
    /// </summary>
    internal interface IModuleType: IModule
    {
        Type? ModuleType { get; }
    }
}
