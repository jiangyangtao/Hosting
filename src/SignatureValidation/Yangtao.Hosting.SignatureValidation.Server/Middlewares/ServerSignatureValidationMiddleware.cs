using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.SignatureValidation.Core.Configurations;
using Yangtao.Hosting.SignatureValidation.Core.Enums;
using Yangtao.Hosting.SignatureValidation.Server.Abstractions;
using Yangtao.Hosting.SignatureValidation.Server.Attributes;

namespace Yangtao.Hosting.SignatureValidation.Server.Middlewares
{
    internal class ServerSignatureValidationMiddleware : IMiddleware
    {
        private readonly IServerConfigurationProvider _serverConfigurationProvider;
        private readonly IServerSignatureValidationProvider _serverSignatureValidationProvider;

        public ServerSignatureValidationMiddleware(
            IServerConfigurationProvider serverConfigurationProvider, 
            IServerSignatureValidationProvider serverSignatureValidationProvider)
        {
            _serverConfigurationProvider = serverConfigurationProvider;
            _serverSignatureValidationProvider = serverSignatureValidationProvider;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var contentType = context.Request.ContentType;
            if (contentType.Contains("grpc", StringComparison.OrdinalIgnoreCase))
            {
                await next(context);
                return;
            }

            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                var hasServerSignatureValidation = endpoint.Metadata.GetMetadata<ServerSignatureValidationAttribute>();
                if (hasServerSignatureValidation != null)
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

                    if (_serverConfigurationProvider.ServerValidationOptions.ValidationType == ValidationType.Signatrue)
                    {
                        var r = _serverSignatureValidationProvider.
                    }
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
