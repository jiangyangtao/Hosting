using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Yangtao.Hosting.Logger.Abstractions;
using Yangtao.Hosting.Logger.Options;
using Yangtao.Hosting.Logger.Services;

namespace Yangtao.Hosting.Logger.LoggerProviders
{
    internal class ChannelLoggerProvider : ILoggerProvider
    {
        private readonly IRealtimeLoggerService? _realtimeLoggerService;
        private readonly FileLoggerWriterService? _fileLogWriterService;
        private readonly LoggerOptions _loggerOptions;

        public ChannelLoggerProvider(IServiceProvider serviceProvider)
        {
            _realtimeLoggerService = serviceProvider.GetService<IRealtimeLoggerService>();
            _fileLogWriterService = serviceProvider.GetService<FileLoggerWriterService>();

            var loggerOptions = serviceProvider.GetRequiredService<IOptionsMonitor<LoggerOptions>>();
            _loggerOptions = loggerOptions.CurrentValue;
            loggerOptions.OnChange(options => _loggerOptions.DeepCopy(options));
        }

        public ILogger CreateLogger(string categoryName) =>
            new ChannelLogger(categoryName, _loggerOptions, _realtimeLoggerService, _fileLogWriterService);

        public void Dispose()
        {
            _fileLogWriterService?.DisposeAsync().AsTask().GetAwaiter().GetResult();
        }
    }

    internal sealed class ChannelLogger : ILogger
    {
        private readonly string _category;
        private readonly IRealtimeLoggerService? _realtimeLoggerService;
        private readonly FileLoggerWriterService? _fileLogWriterService;
        private readonly LoggerOptions _loggerOptions;

        public ChannelLogger(string category, LoggerOptions loggerOptions, IRealtimeLoggerService? realtimeLoggerService, FileLoggerWriterService? fileLogWriterService)
        {
            _category = category;
            _realtimeLoggerService = realtimeLoggerService;
            _fileLogWriterService = fileLogWriterService;
            _loggerOptions = loggerOptions;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

        public bool IsEnabled(LogLevel logLevel)
        {
            if (logLevel != LogLevel.None) return false;
            if (_loggerOptions.EnableChannelLogger == false) return false;
            if (_loggerOptions.CanEnableChannel == false) return false;
            if (_loggerOptions.HasChannelLoggerLevel(logLevel)) return false;

            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
            Exception? exception, Func<TState, Exception?, string> formatter)
        {
            var isIsEnabled = IsEnabled(logLevel);
            if (isIsEnabled) return;

            if (_loggerOptions.RealtimeLoggerOptions != null && _loggerOptions.RealtimeLoggerOptions.HasLevel(logLevel))
            {
                var channelLog = new LogInfo
                {
                    Level = logLevel,
                    Category = _category,
                    Message = formatter(state, exception),
                    Timestamp = DateTimeOffset.Now,
                };

                _realtimeLoggerService?.Publish(channelLog, exception);
            }

            if (_loggerOptions.FileLoggerOptions != null && _loggerOptions.FileLoggerOptions.HasLevel(logLevel))
            {
                var channelLog = new LogInfo
                {
                    Level = logLevel,
                    Category = _category,
                    Message = formatter(state, exception),
                    Timestamp = DateTimeOffset.Now,
                };

                _fileLogWriterService?.Write(channelLog, exception);
            }
        }
    }
}
