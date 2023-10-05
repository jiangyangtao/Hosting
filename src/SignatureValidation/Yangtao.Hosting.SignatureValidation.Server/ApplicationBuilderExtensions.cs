using Microsoft.AspNetCore.Builder;
using Yangtao.Hosting.SignatureValidation.Server.Middlewares;

namespace Yangtao.Hosting.SignatureValidation.Server
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseServerSignatureValidation(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<ServerSignatureValidationMiddleware>();
            return applicationBuilder;
        }
    }
}
