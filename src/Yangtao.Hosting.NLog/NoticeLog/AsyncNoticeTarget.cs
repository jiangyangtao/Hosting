using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Targets;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.NLog.Abstractions;

namespace Yangtao.Hosting.NLog.NoticeLog
{
    [Target("AsyncNoticeLog")]
    internal class AsyncNoticeTarget : AsyncTaskTarget
    {
        private readonly IEnumerable<IAsyncNoticeLogProvider> _noticeLogProviders;

        public AsyncNoticeTarget(IServiceProvider serviceProvider)
        {
            Name = "AsyncNoticeLog";
            _noticeLogProviders = serviceProvider.GetServices<IAsyncNoticeLogProvider>();
        }

        protected override async Task WriteAsyncTask(LogEventInfo logEvent, CancellationToken cancellationToken)
        {
            if (_noticeLogProviders.IsNullOrEmpty()) return;

            foreach (var noticeLogProvider in _noticeLogProviders)
            {
                if (logEvent.Level < noticeLogProvider.MinLevel) continue;
                if (logEvent.Level > noticeLogProvider.MaxLevel) continue;

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
