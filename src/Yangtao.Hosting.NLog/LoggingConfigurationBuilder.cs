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

            var consoleTarget = NLogBuilder.BuildConsoleTarget();    
            loggingConfiguration.AddTarget("console", consoleTarget);
            loggingConfiguration.LoggingRules.Add(new LoggingRule("*", options.ConsoleLogLevel, consoleTarget));

            var fileTargetWrapper = NLogBuilder.BuildFileTarget(options.FileLayoutType);
            loggingConfiguration.AddTarget("file", fileTargetWrapper);           
            loggingConfiguration.LoggingRules.Add(new LoggingRule("*", options.FileLogLevel, fileTargetWrapper));

            return loggingConfiguration;
        }
    }
}