using Microsoft.AspNetCore.Builder;
using Yangtao.Hosting.SignatureValidation.Client.Middlewares;

namespace Yangtao.Hosting.SignatureValidation.Client
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseServerSignatureValidation(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<ClientSignatureValidationMiddleware>();
            return applicationBuilder;
        }
    }
}
