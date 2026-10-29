using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.Logger.Abstractions;
using Yangtao.Hosting.Logger.Options;

namespace Yangtao.Hosting.Logger.LoggerProviders
{
    internal class NoticeLoggerProvider : ILoggerProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEnumerable<ILoggerNoticeProvider> _loggerNoticeProviders;
        private readonly LoggerOptions _loggerOptions;

        public NoticeLoggerProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _loggerNoticeProviders = serviceProvider.GetServices<ILoggerNoticeProvider>();

            var loggerOptions = serviceProvider.GetRequiredService<IOptions<LoggerOptions>>();
            _loggerOptions = loggerOptions.Value ?? throw new NullReferenceException(nameof(LoggerOptions));
        }

        public ILogger CreateLogger(string categoryName) =>
              new NoticeLogger(categoryName, _loggerOptions, _loggerNoticeProviders);

        public void Dispose()
        {
        }
    }

    internal sealed class NoticeLogger : ILogger
    {
        private readonly string _category;
        private readonly LoggerOptions _loggerOptions;
        private readonly IEnumerable<ILoggerNoticeProvider> _loggerNoticeProviders;
        private readonly LogLevel _minNoticeLevel;

        public NoticeLogger(string category, LoggerOptions loggerOptions, IEnumerable<ILoggerNoticeProvider> loggerNoticeProviders)
        {
            _category = category;
            _loggerOptions = loggerOptions;
            _loggerNoticeProviders = loggerNoticeProviders;

            var larkMin = _loggerOptions.LarkNoticeOptions?.MinLevel ?? LogLevel.Error;
            var emailMin = _loggerOptions.EmailNoticeOptions?.MinLevel ?? LogLevel.Error;
            _minNoticeLevel = larkMin < emailMin ? larkMin : emailMin;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

        public bool IsEnabled(LogLevel logLevel) =>
            logLevel != LogLevel.None
            && logLevel >= _minNoticeLevel
            && _loggerNoticeProviders.NotNullAndEmpty();

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            var isEnabled = IsEnabled(logLevel);
            if (isEnabled == false) return;

            if (_loggerNoticeProviders.IsNullOrEmpty()) return;

            var logInfo = new LogInfo
            {
                Level = logLevel,
                Category = _category,
                Message = formatter(state, exception),
                Timestamp = DateTimeOffset.Now,
            };

            foreach (var loggerNoticeProvider in _loggerNoticeProviders)
            {
                loggerNoticeProvider.Notice(logInfo, exception);
            }
        }
    }
}
