using Yangtao.Hosting.GrpcCore;

namespace Yangtao.Hosting.GrpcClient.Options
{
    public class SignAuthenticationOptions
    {
        public bool UseSignAuthenticationGrpcClientInterceptor { set; get; } = false;

        public SignAuthenticationType SignAuthenticationType { set; get; } = SignAuthenticationType.Aes;
    }
}
