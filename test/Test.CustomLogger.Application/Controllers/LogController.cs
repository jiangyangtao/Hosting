using Microsoft.AspNetCore.Mvc;
using Yangtao.Hosting.Logger.Abstractions;

namespace Test.CustomLogger.Application.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LogController : ControllerBase
    {
        private readonly IRealtimeLoggerProvider _realtimeLoggerProvider;

        public LogController(IRealtimeLoggerProvider realtimeLoggerProvider)
        {
            _realtimeLoggerProvider = realtimeLoggerProvider;
        }

        [HttpGet]
        public Task Realtime(CancellationToken cancellationToken) => _realtimeLoggerProvider.SubscribeAsync(HttpContext, cancellationToken);
    }
}
