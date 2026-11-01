using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using Yangtao.Hosting.Email;
using Yangtao.Hosting.NLog.Abstractions;
using Yangtao.Hosting.NLog.ConsoleLog;
using Yangtao.Hosting.NLog.FileLog;
using Yangtao.Hosting.NLog.NoticeLog;
using Yangtao.Hosting.NLog.RealtimeLog;

namespace Yangtao.Hosting.NLog
{
    public static class LoggingBuilderExtensions
    {
        public static ILoggingBuilder AddCustomNLog(this ILoggingBuilder loggingBuilder, string serviceName) => AddCustomNLog(loggingBuilder, options =>
        {
            options.ServiceName = serviceName;
        });

        public static ILoggingBuilder AddCustomNLog(this ILoggingBuilder loggingBuilder, Action<NLogOptions> action)
        {
            var nlogOptions = new NLogOptions();
            action(nlogOptions);

            var services = loggingBuilder.Services;
            services.AddOptions<NLogOptions>().Configure(options => options.DeepCopy(nlogOptions)).ValidateDataAnnotations();

            loggingBuilder.ClearProviders();
            if (nlogOptions.RealtimeLogOptions != null)
            {
                services.AddSingleton<IRealtimeLogProvider, RealtimeLogProvider>();
                services.AddSingleton<LogBufferQueueService>();
                services.AddHostedService(sp => sp.GetRequiredService<LogBufferQueueService>());
            }

            if (nlogOptions.LarkNoticeOptions != null)
            {
                services.AddHttpClient(LarkNoticeOptions.LarkHttpClient);
                services.AddSingleton<LarkNoticeLogProvider>();
            }

            if (nlogOptions.EmailNoticeOptions != null)
            {
                services.AddEmail();
                services.AddSingleton<EmailNoticeLogProvider>();
            }

            loggingBuilder.AddNLog();

            return loggingBuilder;
        }

        public static IApplicationBuilder UseCustomNLog(this IApplicationBuilder applicationBuilder)
        {
            var serviceProvider = applicationBuilder.ApplicationServices;
            var options = serviceProvider.GetRequiredService<IOptions<NLogOptions>>();
            var nlogOptions = options.Value ?? throw new NullReferenceException(nameof(NLogOptions));
            var loggingConfiguration = new LoggingConfiguration();

            if (nlogOptions.ConsoleLogOptions != null)
            {
                var builder = new ConsoleLogBuilder(nlogOptions.ConsoleLogOptions);
                builder.ConfigurationConsoleLog(loggingConfiguration);
            }

            if (nlogOptions.RealtimeLogOptions != null)
            {
                var logBufferQueueService = serviceProvider.GetRequiredService<LogBufferQueueService>();
                var builder = new RealtimeLogBuilder(nlogOptions.RealtimeLogOptions, logBufferQueueService);
                builder.ConfigurationRealtimeLog(loggingConfiguration);
            }

            if (nlogOptions.FileLogOptions != null)
            {
                var builder = new FileLogBuilder(nlogOptions.FileLogOptions);
                builder.ConfigurationFileLog(loggingConfiguration);
            }

            var noticeLogBuilder = new NoticeLogBuilder(nlogOptions, serviceProvider);
            noticeLogBuilder.ConfigurationFileLog(loggingConfiguration);

            LogManager.Configuration = loggingConfiguration;
            var lifetime = applicationBuilder.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
            lifetime.ApplicationStopping.Register(static () => LogManager.Shutdown());

            return applicationBuilder;
        }
    }
}
