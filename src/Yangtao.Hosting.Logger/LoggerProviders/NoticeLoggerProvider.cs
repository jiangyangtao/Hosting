using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.Logger.Abstractions;

namespace Yangtao.Hosting.Logger.LoggerProviders
{
    internal class NoticeLoggerProvider : ILoggerProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public NoticeLoggerProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ILogger CreateLogger(string categoryName) =>
              new NoticeLogger(categoryName, _serviceProvider);

        public void Dispose()
        {
        }
    }

    internal sealed class NoticeLogger : ILogger
    {
        private readonly string _category;
        private readonly IEnumerable<ILoggerNoticeProvider> _loggerNoticeProviders;

        public NoticeLogger(string category, IServiceProvider serviceProvider)
        {
            _category = category;
            _loggerNoticeProviders = serviceProvider.GetServices<ILoggerNoticeProvider>();
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

        public bool IsEnabled(LogLevel logLevel)
        {
            if (logLevel == LogLevel.None) return false;
            if (_loggerNoticeProviders.IsNullOrEmpty()) return false;

            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
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
