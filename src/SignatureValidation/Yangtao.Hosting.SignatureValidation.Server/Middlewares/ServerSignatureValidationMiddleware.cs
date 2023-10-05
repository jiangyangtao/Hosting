using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Yangtao.Hosting.SignatureValidation.Server.Middlewares
{
    internal class ServerSignatureValidationMiddleware : IMiddleware
    {
        public ServerSignatureValidationMiddleware()
        {
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var endpoint = context.GetEndpoint();
            var route = context.GetRouteData();

            await next(context);
        }
    }
}
