using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Targets;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.NLog.Abstractions;

namespace Yangtao.Hosting.NLog.NoticeLog
{
    internal class AsyncTarget : AsyncTaskTarget
    {
        private readonly IEnumerable<INoticeLogProvider> _noticeLogProviders;

        public AsyncTarget(IServiceProvider serviceProvider)
        {
            Name = "AsyncNoticeLog";
            _noticeLogProviders = serviceProvider.GetServices<INoticeLogProvider>();
        }

        protected override async Task WriteAsyncTask(LogEventInfo logEvent, CancellationToken cancellationToken)
        {
            if (_noticeLogProviders.IsNullOrEmpty()) return;

            foreach (var noticeLogProvider in _noticeLogProviders)
            {
                var formatLog = RenderLogEvent(noticeLogProvider.Layout, logEvent);
                try
                {
                    await noticeLogProvider.WriteAsync(formatLog, logEvent, cancellationToken);
                }
                catch (Exception) { }
            }
        }
    }
}
