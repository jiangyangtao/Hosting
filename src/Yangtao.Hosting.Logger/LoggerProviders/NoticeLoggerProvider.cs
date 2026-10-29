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
        private readonly IEnumerable<ILoggerNoticeProvider> _loggerNoticeProviders;
        private readonly LogLevel _minNoticeLevel;

        public NoticeLoggerProvider(IServiceProvider serviceProvider)
        {
            _loggerNoticeProviders = serviceProvider.GetServices<ILoggerNoticeProvider>();

            var options = serviceProvider.GetRequiredService<IOptions<LoggerOptions>>();
            var loggerOptions = options.Value ?? throw new NullReferenceException(nameof(LoggerOptions));

            var larkMin = loggerOptions.LarkNoticeOptions?.MinLevel ?? LogLevel.Error;
            var emailMin = loggerOptions.EmailNoticeOptions?.MinLevel ?? LogLevel.Error;
            _minNoticeLevel = larkMin < emailMin ? larkMin : emailMin;
        }

        public ILogger CreateLogger(string categoryName) =>
              new NoticeLogger(categoryName, _minNoticeLevel, _loggerNoticeProviders);

        public void Dispose()
        {
        }
    }

    internal sealed class NoticeLogger : ILogger
    {
        private readonly string _category;
        private readonly IEnumerable<ILoggerNoticeProvider> _loggerNoticeProviders;
        private readonly LogLevel _minNoticeLevel;

        public NoticeLogger(string category, LogLevel minNoticeLevel, IEnumerable<ILoggerNoticeProvider> loggerNoticeProviders)
        {
            _category = category;
            _minNoticeLevel = minNoticeLevel;
            _loggerNoticeProviders = loggerNoticeProviders;
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
