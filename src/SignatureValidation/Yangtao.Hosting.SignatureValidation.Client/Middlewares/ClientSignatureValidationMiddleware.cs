using Microsoft.AspNetCore.Http;

namespace Yangtao.Hosting.SignatureValidation.Client.Middlewares
{
    internal class ClientSignatureValidationMiddleware : IMiddleware
    {
        public ClientSignatureValidationMiddleware()
        {
        }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}
