using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Text;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.SignatureValidation.Core.Configurations;
using Yangtao.Hosting.SignatureValidation.Core.Enums;
using Yangtao.Hosting.SignatureValidation.Server.Abstractions;
using Yangtao.Hosting.SignatureValidation.Server.Attributes;
using Yangtao.Hosting.SignatureValidation.Server.Configurations;

namespace Yangtao.Hosting.SignatureValidation.Server.Middlewares
{
    internal class ServerSignatureValidationMiddleware : IMiddleware
    {
        private readonly IServerConfigurationProvider _serverConfigurationProvider;
        private readonly IServerSignatureValidationProvider _serverSignatureValidationProvider;
        private readonly IServerEncryptionValidationProvider _serverEncryptionValidationProvider;

        public ServerSignatureValidationMiddleware(
            IServerConfigurationProvider serverConfigurationProvider,
            IServerSignatureValidationProvider serverSignatureValidationProvider,
            IServerEncryptionValidationProvider serverEncryptionValidationProvider)
        {
            _serverConfigurationProvider = serverConfigurationProvider;
            _serverSignatureValidationProvider = serverSignatureValidationProvider;
            _serverEncryptionValidationProvider = serverEncryptionValidationProvider;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Method.Equals(HttpMethod.Get.Method, StringComparison.OrdinalIgnoreCase))
            {
                await next(context);
                return;
            }

            var contentType = context.Request.ContentType;
            if (contentType.IsNullOrEmpty())
            {
                await next(context);
                return;
            }

            if (contentType.Contains(ServerSignatureValidationDefaultKeys.GrpcKey, StringComparison.OrdinalIgnoreCase))
            {
                await next(context);
                return;
            }

            var endpoint = context.GetEndpoint();
            if (endpoint == null)
            {
                await next(context);
                return;
            }

            var hasServerSignatureValidation = endpoint.Metadata.GetMetadata<ServerSignatureValidationAttribute>();
            if (hasServerSignatureValidation == null)
            {
                await next(context);
                return;
            }

            if (_serverConfigurationProvider.ServerValidationOptions.ValidationType == ValidationType.Signatrue)
            {
                var hasSignatureKey = context.Request.Headers.TryGetValue(SignatureValidationDefaultKeys.SignatureKey, out StringValues value);
                if (hasSignatureKey == false)
                {
                    await ValidationFailAsync(context);
                    return;
                }

                var signature = value.ToString();
                if (signature.IsNullOrEmpty())
                {
                    await ValidationFailAsync(context);
                    return;
                }

                context.Request.EnableBuffering();
                var stream = new StreamReader(context.Request.Body);
                var body = await stream.ReadToEndAsync();

                var r = _serverSignatureValidationProvider.VerifyData(body, signature);
                if (r == false)
                {
                    await ValidationFailAsync(context);
                    return;
                }
            }

            if (_serverConfigurationProvider.ServerValidationOptions.ValidationType == ValidationType.Encrypt)
            {
                var stream = new StreamReader(context.Request.Body);
                var body = await stream.ReadToEndAsync();
                if (body.IsNullOrEmpty())
                {
                    await ValidationFailAsync(context);
                    return;
                }

                var content = _serverEncryptionValidationProvider.Decrypt(body);
                if (content.IsNullOrEmpty())
                {
                    await ValidationFailAsync(context);
                    return;
                }

                var requestBody = Encoding.UTF8.GetBytes(content);
                var requestBodyStream = new MemoryStream();
                requestBodyStream.Seek(0, SeekOrigin.Begin);
                requestBodyStream.Write(requestBody, 0, requestBody.Length);
                context.Request.Body = requestBodyStream;
                context.Request.Body.Seek(0, SeekOrigin.Begin);
            }

            await next(context);
        }


        private static async Task ValidationFailAsync(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = 404;
            await httpContext.Response.BodyWriter.FlushAsync().AsTask();
        }
    }
}
