using NLog;
using NLog.Config;

namespace Yangtao.Hosting.NLog
{
    public class LoggingConfigurationBuilder
    {
        public LoggingConfigurationBuilder()
        {
        }

        public static LoggingConfiguration BuildLoggingConfiguration()
        {
            var loggingConfiguration = new LoggingConfiguration();

            var consoleTarget = LogTargetBuilder.BuildConsoleTarget();
            var fileTargetWrapper = LogTargetBuilder.BuildFileTarget();
            loggingConfiguration.AddTarget("console", consoleTarget);
            loggingConfiguration.AddTarget("file", fileTargetWrapper);
            loggingConfiguration.LoggingRules.Add(new LoggingRule("*", LogLevel.Info, consoleTarget));
            loggingConfiguration.LoggingRules.Add(new LoggingRule("*", LogLevel.Error, fileTargetWrapper));

            return loggingConfiguration;
        }
    }
}