using Grpc.Core;
using Test.GrpcServer.Provider;
using Yangtao.Hosting.GrpcServer.Attributes;

namespace Test.GrpcServer.Application.GrpcServices
{
    [SignAuthentication]
    public class AuthenticationSerivce : GrpcValidationProvider.GrpcValidationProviderBase
    {
        public AuthenticationSerivce()
        {
        }

        public override Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
        {
            var response = new LoginResponse
            {
                AccessToken = "AccessToken",
                Result = true,
            };
            return Task.FromResult(response);
        }
    }
}
