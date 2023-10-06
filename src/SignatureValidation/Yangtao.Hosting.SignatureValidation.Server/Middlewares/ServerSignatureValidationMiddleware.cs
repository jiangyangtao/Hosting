using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.SignatureValidation.Core.Configurations;
using Yangtao.Hosting.SignatureValidation.Server.Abstractions;
using Yangtao.Hosting.SignatureValidation.Server.Attributes;

namespace Yangtao.Hosting.SignatureValidation.Server.Middlewares
{
    internal class ServerSignatureValidationMiddleware : IMiddleware
    {
        private readonly IServerConfigurationProvider _serverConfigurationProvider;

        public ServerSignatureValidationMiddleware(IServerConfigurationProvider serverConfigurationProvider)
        {
            _serverConfigurationProvider = serverConfigurationProvider;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
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
