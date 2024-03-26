using NLog;
using NLog.Config;

namespace Yangtao.Hosting.NLog
{
    public class LoggingConfigurationBuilder
    {
        public LoggingConfigurationBuilder()
        {
        }

        public static LoggingConfiguration BuildLoggingConfiguration(NLogConfigurationOptions options)
        {
            var loggingConfiguration = new LoggingConfiguration();

            var consoleTarget = LogTargetBuilder.BuildConsoleTarget();
            var fileTargetWrapper = LogTargetBuilder.BuildFileTarget(options.FileLayoutType);
            loggingConfiguration.AddTarget("console", consoleTarget);
            loggingConfiguration.AddTarget("file", fileTargetWrapper);
            loggingConfiguration.LoggingRules.Add(new LoggingRule("*", LogLevel.Info, consoleTarget));
            loggingConfiguration.LoggingRules.Add(new LoggingRule("*", LogLevel.Error, fileTargetWrapper));

            return loggingConfiguration;
        }
    }
}