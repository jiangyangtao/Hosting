namespace Yangtao.Hosting.FrontendApi.Abstractions
{
    internal interface IBoolean
    {
        /// <summary>
        /// true 描述
        /// </summary>
        string? TrueText { get; }

        /// <summary>
        /// false 描述
        /// </summary>
        string? FalseText { get; }
    }
}
