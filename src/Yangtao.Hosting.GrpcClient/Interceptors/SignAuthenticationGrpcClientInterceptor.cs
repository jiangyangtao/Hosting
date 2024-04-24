using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Options;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.GrpcClient.Options;
using Yangtao.Hosting.GrpcCore.Abstractions;

namespace Yangtao.Hosting.GrpcClient.Interceptors
{
    public class SignAuthenticationGrpcClientInterceptor : Interceptor
    {
        private readonly ISignAuthenticationProvider _signAuthenticationProvider;
        private readonly IOptionsMonitor<GrpcClientOptions> GrpcClientOptions;

        public SignAuthenticationGrpcClientInterceptor(ISignAuthenticationProvider signAuthenticationProvider, IOptionsMonitor<GrpcClientOptions> clientOptions)
        {
            _signAuthenticationProvider = signAuthenticationProvider;
            GrpcClientOptions = clientOptions;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            var exist = GrpcClientOptions.CurrentValue.ExisySignAuthMethods(context.Method.Name);
            if (exist == false) return base.AsyncUnaryCall(request, context, continuation);

            if (context.Options.Headers == null)
            {
                var options = new CallOptions(new Metadata { }, context.Options.Deadline, context.Options.CancellationToken, context.Options.WriteOptions, context.Options.PropagationToken, context.Options.Credentials);
                context = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, options);
            }

            var now = DateTime.Now.ToFormatDateTime();
            var authorization = _signAuthenticationProvider.Encrypt(now);
            context.Options.Headers.Add("Authorization", authorization);

            return base.AsyncUnaryCall(request, context, continuation);
        }
    }
}
