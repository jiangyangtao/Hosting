namespace Yangtao.Hosting.FrontendApi.Abstractions
{
    /// <summary>
    /// 开关接口
    /// </summary>
    internal interface ISwitch
    {
        public object? CheckedValue { get; }

        public object? UnCheckedValue { get; }

        public object? CheckedChildren { get; }

        public object? UnCheckedChildren { get; }
    }
}
