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

        /// <summary>
        /// 获取当前请求 Header 的值
        /// </summary>
        /// <param name="request">HttpRequest 对象</param>
        /// <param name="key">获取 Headers 值对应的 key</param>
        /// <returns>
        /// 如果 request 对象为 null，则返回 <see cref="string.Empty"/>;
        /// 如果 key 参数为 null 或空字符串 ("")，则返回 <see cref="string.Empty"/>;
        /// 如果 <see cref="HttpRequest.Headers"/> 中不包含指定的 key，则返回 <see cref="string.Empty"/>;
        /// 否则返回 <see cref="HttpRequest.Headers"/> 中指定 key 对应的 value 。
        /// </returns>
        public static string GetValue(this HttpRequest request, string key)
        {
            if (request == null) return string.Empty;
            if (key.IsNullOrEmpty()) return string.Empty;
            if (request.Headers.ContainsKey(key) == false) return string.Empty;

            return request.Headers[key].ToString();
        }
    }
}
