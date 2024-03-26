using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Yangtao.Hosting.NLog
{
    public static class LoggingBuilderExtensions
    {
        public static ILoggingBuilder ConfigNLog(this ILoggingBuilder loggingBuilder, Action<NLogConfigurationOptions> action)
        {
            var options = new NLogConfigurationOptions();
            action(options);

            loggingBuilder.ClearProviders();
            var loggingConfiguration = LoggingConfigurationBuilder.BuildLoggingConfiguration(options);
            loggingBuilder.AddNLog(loggingConfiguration);

            return loggingBuilder;
        }
    }
}
