using Grpc.AspNetCore.Server;
using Yangtao.Hosting.GrpcCore.Options;

namespace Yangtao.Hosting.GrpcServer.Options
{
    public class GrpcServerOptions
    {
        internal GrpcServerOptions()
        {
            GrpcServiceOptions = new GrpcServiceOptions();
        }

        public GrpcServiceOptions GrpcServiceOptions {get; }

        public SignAuthenticationType? SignAuthenticationType { set; get; }

        public AesSignOptions? AesSignOptions { set; get; }

        public RsaPrivateSignOptions? RsaPrivateSignOptions { set; get; }
    }
}
