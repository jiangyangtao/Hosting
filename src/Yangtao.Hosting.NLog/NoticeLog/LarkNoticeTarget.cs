using NLog;
using NLog.Targets;

namespace Yangtao.Hosting.NLog.NoticeLog
{
    [Target("LarkNoticeLog")]
    internal class LarkNoticeTarget : AsyncTaskTarget
    {
        private readonly LarkNoticeLogProvider _larkNoticeLogProvider;

        public LarkNoticeTarget(LarkNoticeLogProvider larkNoticeLogProvider, LarkNoticeOptions larkNoticeOptions)
        {
            Name = "LarkNoticeLog";
            _larkNoticeLogProvider = larkNoticeLogProvider;
            Layout = larkNoticeOptions.Layout;
        }

        protected override async Task WriteAsyncTask(LogEventInfo logEvent, CancellationToken cancellationToken)
        {
            var formatLog = RenderLogEvent(Layout, logEvent);
            try
            {
                await _larkNoticeLogProvider.WriteAsync(formatLog, logEvent, cancellationToken);
            }
            catch (Exception) { }
        }
    }
}
