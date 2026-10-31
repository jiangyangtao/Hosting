using NLog;
using NLog.Targets;
using Yangtao.Hosting.NLog.Abstractions;

namespace Yangtao.Hosting.NLog.RealtimeLog
{
    [Target("RealtimeLog")]
    public sealed class RealtimeLogTarget : TargetWithLayout
    {
        private readonly ILogBufferQueueService _logBufferQueueService;

        public RealtimeLogTarget(ILogBufferQueueService logBufferQueueService)
        {
            _logBufferQueueService = logBufferQueueService;
            Name = "Realtime";
            Layout = "${level}: ${longdate} ${newline}      ${callsite}    ${newline}      ${aspnet-request-ip:whenEmpty=localhost} ${newline}      ${message} ${exception:format=tostring}    ${newline}      ${aspnet-request-posted-body}";
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
