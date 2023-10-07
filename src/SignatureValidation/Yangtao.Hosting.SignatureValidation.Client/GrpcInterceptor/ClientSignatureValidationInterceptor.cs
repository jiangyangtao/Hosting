using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Yangtao.Hosting.SignatureValidation.Client.Abstractions;
using Yangtao.Hosting.SignatureValidation.Client.Configurations;

namespace Yangtao.Hosting.SignatureValidation.Client.GrpcInterceptor
{
    internal class ClientSignatureValidationInterceptor : Interceptor
    {
        private readonly GrpcInterceptorOptions GrpcInterceptorOptions;
        private readonly IClientSignatureValidationProvider _clientSignatureValidationProvider;

        public ClientSignatureValidationInterceptor(
            IOptions<GrpcInterceptorOptions> options,
            IClientSignatureValidationProvider clientSignatureValidationProvider)
        {
            GrpcInterceptorOptions = options.Value;
            _clientSignatureValidationProvider = clientSignatureValidationProvider;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            if (request == null) return base.AsyncUnaryCall(request, context, continuation);

            var method = context.Method.Name;
            var exist = GrpcInterceptorOptions.ExistInterceptorMethod(context.Method.Name);
            if (exist == false) return base.AsyncUnaryCall(request, context, continuation);

            context = BuildClientInterceptorContext(request, context);
            return base.AsyncUnaryCall(request, context, continuation);
        }

        private ClientInterceptorContext<TRequest, TResponse> BuildClientInterceptorContext<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context)
            where TRequest : class
            where TResponse : class
        {
            if (context.Options.Headers == null)
            {
                var options = new CallOptions(new Metadata { }, context.Options.Deadline, context.Options.CancellationToken, context.Options.WriteOptions, context.Options.PropagationToken, context.Options.Credentials);
                context = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, options);
            }

            var json = JsonConvert.SerializeObject(request);
            var signature = _clientSignatureValidationProvider.SignData(json);
            context.Options.Headers.Add("signature", signature);

            return context;
        }
    }
}
