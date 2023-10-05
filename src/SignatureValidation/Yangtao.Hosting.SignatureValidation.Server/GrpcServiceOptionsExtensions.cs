using Grpc.AspNetCore.Server;
using Yangtao.Hosting.SignatureValidation.Server.GrpcInterceptor;

namespace Yangtao.Hosting.SignatureValidation.Server
{
    public static class GrpcServiceOptionsExtensions
    {
        public static void AddServerSignatureValidationGrpcInterceptor(this GrpcServiceOptions options)
        {
            options.Interceptors.Add<ServerSignatureValidationInterceptor>();
        }
    }
}
