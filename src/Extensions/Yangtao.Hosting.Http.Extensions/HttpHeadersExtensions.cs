using Microsoft.AspNetCore.Http;

namespace Yangtao.Hosting.Extensions
{
    public static class HttpHeadersExtensions
    {

        /// <summary>
        /// 获取当前请求中指定 Header 的值
        /// </summary>
        /// <param name="request">HttpRequest 对象</param>
        /// <param name="key">获取 Headers 值对应的 key</param>
        /// <returns>
        /// 如果 request 对象为 null，则返回 <see cref="string.Empty"/>;
        /// 如果 key 参数为 null 或空字符串 ("")，则返回 <see cref="string.Empty"/>;
        /// 如果 <see cref="IHeaderDictionary"/> 中不包含指定的 key，则返回 <see cref="string.Empty"/>;
        /// 否则返回 <see cref="IHeaderDictionary"/> 中指定 key 对应的 value 。
        /// </returns>
        public static string GetValue(this IHeaderDictionary headers, string key)
        {
            if (headers == null) return string.Empty;
            if (key.IsNullOrEmpty()) return string.Empty;
            if (headers.ContainsKey(key) == false) return string.Empty;

            return headers[key].ToString();
        }

        public static string Authentication(this IHeaderDictionary headers) => headers.GetValue("Authentication");
    }
}
