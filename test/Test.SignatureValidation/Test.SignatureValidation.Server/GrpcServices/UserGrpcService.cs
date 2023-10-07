using Grpc.Core;
using Test.SignatureValidation.ClientGrpc.Provider;
using Yangtao.Hosting.SignatureValidation.Server.Attributes;

namespace Test.SignatureValidation.Server.GrpcServices
{
    [ServerSignatureValidation]
    public class UserGrpcService : ClientSignatureValidationGrpcProvider.ClientSignatureValidationGrpcProviderBase
    {
        public UserGrpcService()
        {
        }

        public override Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
        {
            return Task.FromResult(new LoginResponse { Result = true, AccessToken = "1111111" });
        }
    }
}
