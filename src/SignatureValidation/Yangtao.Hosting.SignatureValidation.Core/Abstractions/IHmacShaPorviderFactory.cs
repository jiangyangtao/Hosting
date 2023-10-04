namespace Yangtao.Hosting.SignatureValidation.Core.Abstractions
{
    internal interface IHmacShaPorviderFactory
    {
        IHmacShaProvider CreateIHmacShaProvider();
    }
}
