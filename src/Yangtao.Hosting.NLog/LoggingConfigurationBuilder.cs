using NLog.Config;
using Yangtao.Hosting.NLog.Options;

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
            loggingConfiguration.AddTarget("console", consoleTarget);
            loggingConfiguration.LoggingRules.Add(new LoggingRule("*", options.ConsoleLogLevel, consoleTarget));

            var fileTargetWrapper = LogTargetBuilder.BuildFileTarget(options.FileLayoutType);
            loggingConfiguration.AddTarget("file", fileTargetWrapper);           
            loggingConfiguration.LoggingRules.Add(new LoggingRule("*", options.FileLogLevel, fileTargetWrapper));

            return loggingConfiguration;
        }
    }
}