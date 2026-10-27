using Microsoft.Extensions.Logging;

namespace Yangtao.Hosting.Logger.Abstractions
{
    public interface ILogInfo
    {
        public DateTimeOffset Timestamp { get; }

        public LogLevel Level { get; }

        public string Category { get; }

        public string Message { get; }

        public string FormattedText { get; }
    }
}
