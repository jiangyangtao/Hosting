using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Yangtao.Hosting.GrpcClient.Interceptors
{
    /// <summary>
    /// Grpc Client 验证拦截器
    /// </summary>
    public class AuthenticationGrpcClientInterceptor : Interceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 构建 <see cref="AuthenticationGrpcClientInterceptor"/>
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public AuthenticationGrpcClientInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
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
