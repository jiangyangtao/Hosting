using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Yangtao.Hosting.SignatureValidation.Client.Abstractions;
using Yangtao.Hosting.SignatureValidation.Client.Configurations;
using Yangtao.Hosting.SignatureValidation.Core.Configurations;
using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Client.GrpcInterceptor
{
    internal class ClientSignatureValidationInterceptor : Interceptor
    {
        private readonly GrpcInterceptorOptions GrpcInterceptorOptions;
        private readonly IClientConfigurationProvider _clientConfigurationProvider;
        private readonly IClientEncryptionValidationProvider _clientEncryptionValidationProvider;
        private readonly IClientSignatureValidationProvider _clientSignatureValidationProvider;

        public ClientSignatureValidationInterceptor(
            IOptions<GrpcInterceptorOptions> options,
            IClientSignatureValidationProvider clientSignatureValidationProvider,
            IClientEncryptionValidationProvider clientEncryptionValidationProvider,
            IClientConfigurationProvider clientConfigurationProvider)
        {
            GrpcInterceptorOptions = options.Value;
            _clientSignatureValidationProvider = clientSignatureValidationProvider;
            _clientEncryptionValidationProvider = clientEncryptionValidationProvider;
            _clientConfigurationProvider = clientConfigurationProvider;
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
            var signature = BuildSignature(json);
            context.Options.Headers.Add(SignatureValidationDefaultKeys.SignatureKey, signature);

            return context;
        }

        private string BuildSignature<TRequest>(TRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            if (_clientConfigurationProvider.ClientValidationOptions.ValidationType == ValidationType.Signatrue) return _clientSignatureValidationProvider.SignData(json);

            return _clientEncryptionValidationProvider.Encrypt(json);
        }
    }
}
