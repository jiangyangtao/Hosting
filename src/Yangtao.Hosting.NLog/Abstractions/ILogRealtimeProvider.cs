using Microsoft.AspNetCore.Http;

namespace Yangtao.Hosting.NLog.Abstractions
{
    /// <summary>
    /// 实时日志提供者
    /// </summary>
    public interface ILogRealtimeProvider
    {
        Task SubscribeAsync(HttpContext context, CancellationToken cancellationToken);
    }
}
