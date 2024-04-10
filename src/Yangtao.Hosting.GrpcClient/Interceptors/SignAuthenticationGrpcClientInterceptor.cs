using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Options;

namespace Yangtao.Hosting.GrpcClient.Interceptors
{
    public class SignAuthenticationGrpcClientInterceptor : Interceptor
    {
        private readonly IOptions<SignAuthenticationOptions> _signAuthenticationOptions;

        public SignAuthenticationGrpcClientInterceptor(IOptions<SignAuthenticationOptions> signAuthenticationOptions)
        {
            _signAuthenticationOptions = signAuthenticationOptions;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            if (context.Options.Headers == null)
            {
                var options = new CallOptions(new Metadata { }, context.Options.Deadline, context.Options.CancellationToken, context.Options.WriteOptions, context.Options.PropagationToken, context.Options.Credentials);
                context = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, options);
            }

            return base.AsyncUnaryCall(request, context, continuation);
        }
    }
}
