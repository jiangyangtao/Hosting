using System.ComponentModel.DataAnnotations;
using Yangtao.Hosting.NLog.ConsoleLog;
using Yangtao.Hosting.NLog.FileLog;
using Yangtao.Hosting.NLog.NoticeLog;
using Yangtao.Hosting.NLog.RealtimeLog;

namespace Yangtao.Hosting.NLog
{
    public class NLogConfigurationOptions
    {
        internal NLogConfigurationOptions() { }

        [Required]
        public string ServiceName { set; get; } = string.Empty;

        public RealtimeLogOptions? RealtimeLogOptions { set; get; } = new RealtimeLogOptions();

        public FileLogOptions FileLogOptions { set; get; } = new FileLogOptions();

        public ConsoleLogOptions ConsoleLogOptions { set; get; } = new ConsoleLogOptions();

        public LarkNoticeOptions? LarkNoticeOptions { set; get; } = null;

        public EmailNoticeOptions? EmailNoticeOptions { set; get; } = null;
    }
}
