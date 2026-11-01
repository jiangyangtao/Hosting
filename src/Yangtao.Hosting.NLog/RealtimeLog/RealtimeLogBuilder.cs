using NLog.Config;
using NLog.Layouts;

namespace Yangtao.Hosting.NLog.RealtimeLog
{
    internal class RealtimeLogBuilder
    {
        private readonly RealtimeLogOptions _realtimeLogOptions;
        private readonly LogBufferQueueService _logBufferQueueService;

        public RealtimeLogBuilder(RealtimeLogOptions realtimeLogOptions, LogBufferQueueService logBufferQueueService)
        {
            _realtimeLogOptions = realtimeLogOptions;
            _logBufferQueueService = logBufferQueueService;
        }

        public RealtimeLogTarget RealtimeLogTarget => new(_logBufferQueueService, _realtimeLogOptions);

        public LoggingRule RealtimeLogRule => _realtimeLogOptions.BuildLoggingRule(RealtimeLogTarget);

        public LoggingConfiguration ConfigurationRealtimeLog(LoggingConfiguration configuration)
        {
            configuration.AddTarget(RealtimeLogTarget);
            configuration.LoggingRules.Add(RealtimeLogRule);

            return configuration;
        }

        internal static Layout DefaultLayout = "${level}: ${longdate} ${newline}      ${callsite}    ${newline}      ${aspnet-request-ip:whenEmpty=localhost} ${newline}      ${message} ${exception:format=tostring}    ${newline}      ${aspnet-request-posted-body}";
    }
}
