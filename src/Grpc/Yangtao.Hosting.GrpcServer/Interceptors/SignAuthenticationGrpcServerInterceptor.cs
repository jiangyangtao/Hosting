using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.GrpcCore.Abstractions;
using Yangtao.Hosting.GrpcServer.Attributes;
using Yangtao.Hosting.GrpcServer.Options;

namespace Yangtao.Hosting.GrpcServer.Interceptors
{
    internal class SignAuthenticationGrpcServerInterceptor : Interceptor
    {
        private readonly ISignAuthenticationProvider _signAuthenticationProvider;
        private readonly IOptions<GrpcServerOptions> _grpcServerOptions;
        private readonly ILogger<SignAuthenticationGrpcServerInterceptor> _logger;

        public SignAuthenticationGrpcServerInterceptor(
            ISignAuthenticationProvider signAuthenticationProvider,
            IOptions<GrpcServerOptions> grpcServerOptions,
            ILogger<SignAuthenticationGrpcServerInterceptor> logger)
        {
            _signAuthenticationProvider = signAuthenticationProvider;
            _grpcServerOptions = grpcServerOptions;
            _logger = logger;
        }

        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            var httpContext = context.GetHttpContext();
            var endpoint = httpContext.GetEndpoint();
            if (endpoint == null) return base.UnaryServerHandler(request, context, continuation);

            var signAuthentication = endpoint.Metadata.GetMetadata<SignAuthenticationAttribute>();
            if (signAuthentication == null) return base.UnaryServerHandler(request, context, continuation);

            var authorization = httpContext.Request.Headers.GetAuthorization();
            if (authorization.IsNullOrEmpty())
            {
                _logger.LogWarning("Authorization for the request header is empty, endpoint:{endpoint}.", endpoint.DisplayName);
                throw UnauthorizedException;
            }

            var time = _signAuthenticationProvider.Decrypt(authorization, _grpcServerOptions.Value.OptionName);
            var parseResult = DateTime.TryParse(time, out DateTime d);
            if (parseResult == false)
            {
                _logger.LogWarning("Authorization conversion DateTime failed, endpoint:{endpoint}.", endpoint.DisplayName);
                throw UnauthorizedException;
            }

            var diff = DateTime.Now.Subtract(d);
            if (diff.TotalSeconds > 5)
            {
                _logger.LogWarning("Authorization timeout, total seconds:{TotalSeconds}, endpoint:{endpoint}.", diff.TotalSeconds, endpoint.DisplayName);
                throw UnauthorizedException;
            }

            return base.UnaryServerHandler(request, context, continuation);
        }

        private static RpcException UnauthorizedException => new(Status.DefaultCancelled, "Unauthorized");
    }
}
