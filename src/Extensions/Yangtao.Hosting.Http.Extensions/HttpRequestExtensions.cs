using Microsoft.AspNetCore.Http;
using System.Text;

namespace Yangtao.Hosting.Extensions
{
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// 获取当前请求的站点
        /// </summary>
        /// <param name="request">当前请求</param>
        /// <returns>返回当前站点</returns>
        public static string GetOriginHost(this HttpRequest request)
        {
            if (request == null) return string.Empty;

            return $"{request.Scheme}://{request.Host}";
        }

        /// <summary>
        /// 获取当前绝对 Url
        /// </summary>
        /// <param name="request">当前请求</param>
        /// <returns>当前请求的 Url，包含参数</returns>
        public static string GetAbsoluteUrl(this HttpRequest request)
        {
            if (request == null) return string.Empty;
            return new StringBuilder()
                .Append(request.Scheme)
                .Append("://")
                .Append(request.Host)
                .Append(request.PathBase)
                .Append(request.Path)
                .Append(request.QueryString)
                .ToString();
        }

        public static string GetAuthorization(this HttpRequest request)
        {
            if (request == null) return string.Empty;
            if (request.Headers.IsNullOrEmpty()) return string.Empty;

            return request.Headers.GetAuthorization();
        }
    }
}
