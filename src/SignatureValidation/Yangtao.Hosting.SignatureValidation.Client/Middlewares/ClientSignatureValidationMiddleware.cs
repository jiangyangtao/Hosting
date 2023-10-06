using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.SignatureValidation.Client.Abstractions;
using Yangtao.Hosting.SignatureValidation.Client.Attributes;
using Yangtao.Hosting.SignatureValidation.Client.Configurations;
using Yangtao.Hosting.SignatureValidation.Core.Configurations;
using Yangtao.Hosting.SignatureValidation.Core.Enums;

namespace Yangtao.Hosting.SignatureValidation.Client.Middlewares
{
    internal class ClientSignatureValidationMiddleware : IMiddleware
    {
        private readonly IClientConfigurationProvider _clientConfigurationProvider;
        private readonly IClientSignatureValidationProvider _clientSignatureValidationProvider;

        public ClientSignatureValidationMiddleware(
            IClientConfigurationProvider clientConfigurationProvider,
            IClientSignatureValidationProvider clientSignatureValidationProvider)
        {
            _clientConfigurationProvider = clientConfigurationProvider;
            _clientSignatureValidationProvider = clientSignatureValidationProvider;
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

            if (contentType.Contains(ClientSignatureValidationDefaultKeys.GrpcKey, StringComparison.OrdinalIgnoreCase))
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

            var hasClientSignatureValidation = endpoint.Metadata.GetMetadata<ClientSignatureValidationAttribute>();
            if (hasClientSignatureValidation == null)
            {
                await next(context);
                return;
            }

            if (_clientConfigurationProvider.ClientValidationOptions.ValidationType == ValidationType.Signatrue)
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

                var r = _clientSignatureValidationProvider.VerifyData(body, signature);
                if (r == false)
                {
                    await ValidationFailAsync(context);
                    return;
                }
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
