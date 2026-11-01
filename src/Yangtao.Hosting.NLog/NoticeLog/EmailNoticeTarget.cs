using NLog;
using NLog.Targets;

namespace Yangtao.Hosting.NLog.NoticeLog
{
    [Target("EmailNoticeLog")]
    internal class EmailNoticeTarget : AsyncTaskTarget
    {
        private readonly EmailNoticeLogProvider _emailNoticeLogProvider;

        public EmailNoticeTarget(EmailNoticeLogProvider emailNoticeLogProvider, EmailNoticeOptions emailNoticeOptions)
        {
            Name = "EmailNoticeLog";
            _emailNoticeLogProvider = emailNoticeLogProvider;

            Layout = emailNoticeOptions.Layout;
        }

        protected override async Task WriteAsyncTask(LogEventInfo logEvent, CancellationToken cancellationToken)
        {
            var formatLog = RenderLogEvent(Layout, logEvent);
            try
            {
                await _emailNoticeLogProvider.WriteAsync(formatLog, logEvent, cancellationToken);
            }
            catch (Exception) { }
        }
    }
}
