using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Yangtao.Hosting.Email;
using Yangtao.Hosting.Logger.Abstractions;
using Yangtao.Hosting.Logger.LoggerProviders;
using Yangtao.Hosting.Logger.Options;
using Yangtao.Hosting.Logger.Providers;
using Yangtao.Hosting.Logger.Services;
using Yangtao.Hosting.MessageQueue.BufferQueue;

namespace Yangtao.Hosting.Logger
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomLogger(this IServiceCollection services, Action<LoggerOptions> loggerOptionsAction)
        {
            services.AddEmail();

            var loggerOptions = new LoggerOptions();
            loggerOptionsAction(loggerOptions);

            services.AddOptions<LoggerOptions>().Configure(options => options.DeepCopy(loggerOptions)).ValidateDataAnnotations().ValidateOnStart();

            if (loggerOptions.EnableChannelLogger && loggerOptions.CanEnableChannel)
            {
                if (loggerOptions.RealtimeLoggerOptions != null)
                {
                    services.AddSingleton<IRealtimeLoggerService, RealtimeLoggerService>();
                }

                if (loggerOptions.FileLoggerOptions != null)
                {
                    services.AddSingleton<FileLoggerWriterService>();
                    services.AddHostedService(serviceProvider => serviceProvider.GetRequiredService<FileLoggerWriterService>());
                }

                services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, ChannelLoggerProvider>());
            }

            if (loggerOptions.EnableLoggerNotice && loggerOptions.CanEnableNotice)
            {
                services.AddHttpClient();
                services.AddBufferQueue();
                services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, NoticeLoggerProvider>());

                if (loggerOptions.EmailNoticeOptions != null) services.AddSingleton<ILoggerNoticeProvider, EmailNoticeProvider>();
                if (loggerOptions.LarkNoticeOptions != null) services.AddSingleton<ILoggerNoticeProvider, LarkNoticeProvider>();
            }

            return services;
        }
    }
}
