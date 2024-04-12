using Microsoft.AspNetCore.Http;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.Http.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static string GetClaimValue(this IHttpContextAccessor contextAccessor, string key)
        {
            if (contextAccessor == null) return string.Empty;
            if (contextAccessor.HttpContext == null) return string.Empty;
            if (contextAccessor.HttpContext.User == null) return string.Empty;

            var claims = contextAccessor.HttpContext.User.Claims;
            if (claims.IsNullOrEmpty()) return string.Empty;

            var r = claims.FirstOrDefault(a => a.Type == key);
            if (r == null) return string.Empty;

            return r.Value;
        }

        public static string GetUserId(this IHttpContextAccessor contextAccessor) => contextAccessor.GetClaimValue("UserId");
    }
}
