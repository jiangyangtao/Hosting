using NLog.Config;
using Yangtao.Hosting.NLog.Abstractions;

namespace Yangtao.Hosting.NLog.RealtimeLog
{
    internal class RealtimeLogBuilder
    {
        private readonly RealtimeLogOptions _realtimeLogOptions;
        private readonly ILogBufferQueueService _logBufferQueueService;

        public RealtimeLogBuilder(RealtimeLogOptions realtimeLogOptions, ILogBufferQueueService logBufferQueueService)
        {
            _realtimeLogOptions = realtimeLogOptions;
            _logBufferQueueService = logBufferQueueService;
        }

        public RealtimeLogTarget RealtimeLogTarget => new(_logBufferQueueService);

        public LoggingRule RealtimeLogRule => _realtimeLogOptions.BuildLoggingRule(RealtimeLogTarget);

        public LoggingConfiguration ConfigurationFileLog(LoggingConfiguration configuration)
        {
            configuration.AddTarget(RealtimeLogTarget);
            configuration.LoggingRules.Add(RealtimeLogRule);

            return configuration;
        }
    }
}
