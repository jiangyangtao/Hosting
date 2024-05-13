using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.AspNetCore.Http;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.GrpcCore.Abstractions;
using Yangtao.Hosting.GrpcServer.Attributes;

namespace Yangtao.Hosting.GrpcServer.Interceptors
{
    internal class SignAuthenticationGrpcServerInterceptor : Interceptor
    {
        private readonly ISignAuthenticationProvider _signAuthenticationProvider;

        public SignAuthenticationGrpcServerInterceptor(ISignAuthenticationProvider signAuthenticationProvider)
        {
            _signAuthenticationProvider = signAuthenticationProvider;
        }

        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            var httpContext = context.GetHttpContext();
            var endpoint = httpContext.GetEndpoint();
            if (endpoint == null) return base.UnaryServerHandler(request, context, continuation);

            var signAuthentication = endpoint.Metadata.GetMetadata<SignAuthenticationAttribute>();
            if (signAuthentication == null) return base.UnaryServerHandler(request, context, continuation);

            if (httpContext.Request.Headers.Authorization.IsNullOrEmpty()) throw UnauthorizedException;

            var authorization = httpContext.Request.Headers.Authorization.ToString();
            var time = _signAuthenticationProvider.Decrypt(authorization);

            var parseResult = DateTime.TryParse(time, out DateTime d);
            if (parseResult == false) throw UnauthorizedException;

            var diff = DateTime.Now.Subtract(d);
            if (diff.TotalSeconds > 10) throw UnauthorizedException;

            return base.UnaryServerHandler(request, context, continuation);
        }

        private RpcException UnauthorizedException => new(Status.DefaultCancelled, "Unauthorized");
    }
}
