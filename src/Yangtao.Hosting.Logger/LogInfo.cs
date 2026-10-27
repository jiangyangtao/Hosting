using Microsoft.Extensions.Logging;
using Yangtao.Hosting.Logger.Abstractions;

namespace Yangtao.Hosting.Logger
{
    internal class LogInfo: ILogInfo
    {
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.Now;

        public LogLevel Level { get; set; }

        public string Category { get; set; } = default!;

        public string Message { get; set; } = default!;

        public string FormattedText { get; set; } = default!; // 已格式化好的字符串
    }
}
