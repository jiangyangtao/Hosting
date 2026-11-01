using NLog;
using NLog.Layouts;

namespace Yangtao.Hosting.NLog.Abstractions
{
    public interface IAsyncNoticeLogProvider
    {
        Layout Layout { get; }

        LogLevel MinLevel { get; }

        LogLevel MaxLevel { get; }

        Task WriteAsync(string formatLog, LogEventInfo logEvent, CancellationToken cancellationToken);
    }
}
