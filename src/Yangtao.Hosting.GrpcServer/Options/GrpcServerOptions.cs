using Grpc.AspNetCore.Server;
using Yangtao.Hosting.GrpcCore.Options;

namespace Yangtao.Hosting.GrpcServer.Options
{
    public class GrpcServerOptions
    {
        internal GrpcServerOptions()
        {
        }

        public GrpcServiceOptions GrpcServiceOptions { set; get; }

        public SignAuthenticationType? SignAuthenticationType { set; get; }

        public AesSignOptions? AesSignOptions { set; get; }

        public RsaPrivateSignOptions? RsaPrivateSignOptions { set; get; }
    }
}
