using Yangtao.Hosting.GrpcCore.Options;

namespace Yangtao.Hosting.GrpcCore.Abstractions
{
    internal interface ISignProvider : ISignAuthenticationProvider
    {
        SignAuthenticationType SignAuthenticationType { get; }
    }
}
