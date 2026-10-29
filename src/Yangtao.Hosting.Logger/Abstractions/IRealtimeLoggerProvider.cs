using Microsoft.AspNetCore.Http;

namespace Yangtao.Hosting.Logger.Abstractions
{
    public interface IRealtimeLoggerProvider
    {
        Task SubscribeAsync(HttpContext context, CancellationToken cancellationToken);
    }
}
