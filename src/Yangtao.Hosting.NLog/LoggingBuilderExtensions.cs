using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Yangtao.Hosting.NLog
{
    public static class LoggingBuilderExtensions
    {
        public static ILoggingBuilder ConfigNLog(this ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.ClearProviders();

            var loggingConfiguration = LoggingConfigurationBuilder.BuildLoggingConfiguration();
            loggingBuilder.AddNLog(loggingConfiguration);

            return loggingBuilder;
        }
    }
}
