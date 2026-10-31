using NLog;
using NLog.Layouts;

namespace Yangtao.Hosting.NLog.Abstractions
{
    public interface INoticeLogProvider
    {
        Layout Layout { get; }

        Task WriteAsync(string formatLog, LogEventInfo logEvent, CancellationToken cancellationToken);
    }
}
