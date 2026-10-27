using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Yangtao.Hosting.Logger.Abstractions;
using Yangtao.Hosting.Logger.LoggerProviders;
using Yangtao.Hosting.Logger.Options;
using Yangtao.Hosting.Logger.Providers;
using Yangtao.Hosting.Logger.Services;

namespace Yangtao.Hosting.Logger
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomLogger(this IServiceCollection services, Action<LoggerOptions> loggerOptionsAction)
        {
            var loggerOptions = new LoggerOptions();
            loggerOptionsAction(loggerOptions);

            services.AddOptions<LoggerOptions>().Configure(options => options.DeepCopy(loggerOptions)).ValidateDataAnnotations();

            services.AddSingleton<ILoggerProvider, ChannelLoggerProvider>();
            services.AddSingleton<ILoggerProvider, FileLoggerProvider>();

            services.AddSingleton<ILoggerChannelService, LoggerChannelService>();
            services.AddSingleton<ILoggerNoticeService, LoggerNoticeService>();

            services.AddSingleton<ILoggerNoticeProvider, EmailNoticeProvider>();
            services.AddSingleton<ILoggerNoticeProvider, LarkNoticeProvider>();

            return services;
        }
    }
}
