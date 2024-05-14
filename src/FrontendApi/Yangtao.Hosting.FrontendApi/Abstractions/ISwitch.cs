namespace Yangtao.Hosting.FrontendApi.Abstractions
{
    internal interface ISwitch
    {
        public object? CheckedValue { get; }

        public object? UnCheckedValue { get; }

        public object? CheckedChildren { get; }

        public object? UnCheckedChildren { get; }
    }
}
