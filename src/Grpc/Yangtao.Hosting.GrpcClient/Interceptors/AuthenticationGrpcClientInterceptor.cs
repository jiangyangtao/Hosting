using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.GrpcClient.Interceptors
{
    /// <summary>
    /// Grpc Client 验证拦截器
    /// </summary>
    public class AuthenticationGrpcClientInterceptor : Interceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string authorizationKey = "authorization";
        private readonly ILogger<AuthenticationGrpcClientInterceptor> _logger;

        /// <summary>
        /// 构建 <see cref="AuthenticationGrpcClientInterceptor"/>
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="logger"></param>
        public AuthenticationGrpcClientInterceptor(IHttpContextAccessor httpContextAccessor, ILogger<AuthenticationGrpcClientInterceptor> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        /// <summary>
        /// 重写 AsyncUnaryCall 加入验证信息
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <param name="continuation"></param>
        /// <returns></returns>
        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            if (context.Options.Headers == null)
            {
                var options = new CallOptions(new Metadata { }, context.Options.Deadline, context.Options.CancellationToken, context.Options.WriteOptions, context.Options.PropagationToken, context.Options.Credentials);
                context = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, options);
            }

            var authorization = context.Options.Headers.FirstOrDefault(a => a.Key == authorizationKey);
            if (authorization == null || authorization.Value.IsNullOrEmpty())
            {
                var token = GetAuthorization();
                if (token.NotNullAndEmpty()) context.Options.Headers.Add(HeaderNames.Authorization, token);
            }

            _logger.LogInformation("Request {method} match native authentication.", context.Method.FullName);
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
            var r = headers.TryGetValue(HeaderNames.Authorization, out StringValues _value);
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
