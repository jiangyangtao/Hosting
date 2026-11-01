using Microsoft.AspNetCore.Mvc;
using Yangtao.Hosting.NLog.Abstractions;

namespace Test.CustomLogger.Application.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LogController : ControllerBase
    {
        private readonly IRealtimeLogProvider _realtimeLogProvider;

        public LogController(IRealtimeLogProvider realtimeLogProvider)
        {
            _realtimeLogProvider = realtimeLogProvider;
        }

        [HttpGet]
        public Task Realtime(CancellationToken cancellationToken) => _realtimeLogProvider.SubscribeAsync(HttpContext, cancellationToken);
    }
}
