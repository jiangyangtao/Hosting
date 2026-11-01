using Microsoft.AspNetCore.Http;

namespace Yangtao.Hosting.NLog.Abstractions
{
    /// <summary>
    /// 实时日志提供者
    /// </summary>
    public interface IRealtimeLogProvider
    {
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SubscribeAsync(HttpContext context, CancellationToken cancellationToken);
    }
}
