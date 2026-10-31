using Yangtao.Hosting.NLog.ConsoleLog;
using Yangtao.Hosting.NLog.FileLog;
using Yangtao.Hosting.NLog.NoticeLog;
using Yangtao.Hosting.NLog.RealtimeLog;

namespace Yangtao.Hosting.NLog
{
    public class NLogConfigurationOptions
    {
        internal NLogConfigurationOptions() { }

        public RealtimeLogOptions? RealtimeLogOptions { set; get; } = new RealtimeLogOptions();

        public FileLogOptions FileLogOptions { set; get; } = new FileLogOptions();

        public ConsoleLogOptions ConsoleLogOptions { set; get; } = new ConsoleLogOptions();

        public LarkNoticeOptions? LarkNoticeOptions { set; get; }

        public EmailNoticeOptions? EmailNoticeOptions { set; get; }
    }
}
