using Microsoft.AspNetCore.Http;

namespace Yangtao.Hosting.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetAuthorization(this HttpContext context)
        {
            if (context == null) return string.Empty;
            if (context.Request == null) return string.Empty;

            return context.Request.GetAuthorization();
        }
    }
}
