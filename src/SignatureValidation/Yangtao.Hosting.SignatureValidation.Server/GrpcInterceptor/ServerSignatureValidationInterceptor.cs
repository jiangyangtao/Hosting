using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.SignatureValidation.Core.Configurations;
using Yangtao.Hosting.SignatureValidation.Core.Enums;
using Yangtao.Hosting.SignatureValidation.Server.Abstractions;
using Yangtao.Hosting.SignatureValidation.Server.Attributes;

namespace Yangtao.Hosting.SignatureValidation.Server.GrpcInterceptor
{
    internal class ServerSignatureValidationInterceptor : Interceptor
    {
        private readonly IServerConfigurationProvider _serverConfigurationProvider;
        private readonly IServerSignatureValidationProvider _serverSignatureValidationProvider;
        private readonly IServerEncryptionValidationProvider _serverEncryptionValidationProvider;

        public ServerSignatureValidationInterceptor(
            IServerConfigurationProvider serverConfigurationProvider,
            IServerSignatureValidationProvider serverSignatureValidationProvider,
            IServerEncryptionValidationProvider serverEncryptionValidationProvider)
        {
            _serverConfigurationProvider = serverConfigurationProvider;
            _serverSignatureValidationProvider = serverSignatureValidationProvider;
            _serverEncryptionValidationProvider = serverEncryptionValidationProvider;
        }

        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            if (request == null) return base.UnaryServerHandler(request, context, continuation); ;

            var httpContext = context.GetHttpContext();
            var endpoint = httpContext.GetEndpoint();
            if (endpoint == null) return base.UnaryServerHandler(request, context, continuation); ;

            var hasServerSignatureValidation = endpoint.Metadata.GetMetadata<ServerSignatureValidationAttribute>();
            if (hasServerSignatureValidation == null) return base.UnaryServerHandler(request, context, continuation);

            var signature = context.RequestHeaders.GetValue(SignatureValidationDefaultKeys.SignatureKey);
            if (signature.IsNullOrEmpty()) throw new RpcException(new Status(StatusCode.NotFound, string.Empty));
            var body = JsonConvert.SerializeObject(request);

            if (_serverConfigurationProvider.ServerValidationOptions.ValidationType == ValidationType.Signatrue)
            {
                var r = _serverSignatureValidationProvider.VerifyData(body, signature);
                if (r == false) throw new RpcException(new Status(StatusCode.NotFound, string.Empty));
            }

            if (_serverConfigurationProvider.ServerValidationOptions.ValidationType == ValidationType.Encrypt)
            {
                var jsonContent = _serverEncryptionValidationProvider.Decrypt(signature);
                if (jsonContent.IsNullOrEmpty()) throw new RpcException(new Status(StatusCode.NotFound, string.Empty));
                if (jsonContent != body) throw new RpcException(new Status(StatusCode.NotFound, string.Empty));
            }

            return base.UnaryServerHandler(request, context, continuation);
        }
    }
}
