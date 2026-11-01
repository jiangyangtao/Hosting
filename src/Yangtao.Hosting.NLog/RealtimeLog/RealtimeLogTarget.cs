using NLog;
using NLog.Targets;
using Yangtao.Hosting.NLog.Abstractions;

namespace Yangtao.Hosting.NLog.RealtimeLog
{
    [Target("RealtimeLog")]
    internal sealed class RealtimeLogTarget : TargetWithLayout
    {
        private readonly ILogBufferQueueService _logBufferQueueService;

        public RealtimeLogTarget(ILogBufferQueueService logBufferQueueService, RealtimeLogOptions realtimeLogOptions)
        {
            _logBufferQueueService = logBufferQueueService;
            Name = "RealtimeLog";

            var layout = realtimeLogOptions.Layout ?? RealtimeLogBuilder.DefaultLayout;
            Layout = layout;
        }

        protected override void Write(LogEventInfo logEvent)
        {
            var message = RenderLogEvent(Layout, logEvent);
            var logInfo = new LogInfo(
                Timestamp: logEvent.TimeStamp,
                Level: logEvent.Level.Name,
                LoggerName: logEvent.LoggerName ?? string.Empty,
                Message: message,
                Exception: logEvent.Exception?.ToString());

            _logBufferQueueService.TryPublish(logInfo);
        }
    }
}
