using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Yangtao.Hosting.GrpcClient.Interceptors
{
    public class AuthenticationGrpcClientInterceptor : Interceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationGrpcClientInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            if (context.Options.Headers == null)
            {
                var options = new CallOptions(new Metadata { }, context.Options.Deadline, context.Options.CancellationToken, context.Options.WriteOptions, context.Options.PropagationToken, context.Options.Credentials);
                context = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, options);
            }

            var authorization = GetAuthorization();
            context.Options.Headers.Add("Authorization", authorization);
            return base.AsyncUnaryCall(request, context, continuation);
        }

        private string GetAuthorization()
        {
            if (_httpContextAccessor == null) return string.Empty;
            if (_httpContextAccessor.HttpContext == null) return string.Empty;
            if (_httpContextAccessor.HttpContext.Request == null) return string.Empty;
            if (_httpContextAccessor.HttpContext.Request.Headers == null) return string.Empty;
            if (_httpContextAccessor.HttpContext.Request.Headers.Any() == false) return string.Empty;

            var headers = _httpContextAccessor.HttpContext.Request.Headers;
            var r = headers.TryGetValue("Authorization", out StringValues _value);
            if (r == false) return string.Empty;

            var removeChar = "Bearer";
            var value = _value.ToString();
            if (value.StartsWith(removeChar, StringComparison.OrdinalIgnoreCase))
            {
                value = value[removeChar.Length..].Trim();
            }

            return value;
        }
    }
}
