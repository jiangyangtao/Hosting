using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Yangtao.Hosting.Email;
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
            services.AddEmail();

            var loggerOptions = new LoggerOptions();
            loggerOptionsAction(loggerOptions);

            services.AddOptions<LoggerOptions>().Configure(options => options.DeepCopy(loggerOptions)).ValidateDataAnnotations();
            var serviceProvider = services.BuildServiceProvider();

            if (loggerOptions.EnableChannelLogger && loggerOptions.CanEnableChannel)
            {
                if (loggerOptions.RealtimeLoggerOptions != null)
                {
                    services.AddSingleton<IRealtimeLoggerService, RealtimeLoggerService>();
                }

                if (loggerOptions.FileLoggerOptions != null)
                {
                    services.AddSingleton<FileLoggerWriterService>();
                }

                services.TryAddSingleton<ILoggerProvider, ChannelLoggerProvider>();
            }

            if (loggerOptions.EnableLoggerNotice && loggerOptions.CanEnableNotice)
            {
                services.AddSingleton<ILoggerProvider, NoticeLoggerProvider>();
                services.AddSingleton<ILoggerNoticeService, LoggerNoticeService>();

                services.AddSingleton<ILoggerNoticeProvider, EmailNoticeProvider>();
                services.AddSingleton<ILoggerNoticeProvider, LarkNoticeProvider>();
            }

            return services;
        }
    }
}
