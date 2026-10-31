using NLog;
using System.ComponentModel.DataAnnotations;

namespace Yangtao.Hosting.NLog.NoticeLog
{
    public class LarkNoticeOptions : NoticeOptionsBase
    {
        [Required]
        public string? WebhookUrl { set; get; }

        public string? Secret { set; get; }

        public override LogLevel MinLevel { set; get; } = LogLevel.Warn;

        public override LogLevel MaxLevel { set; get; } = LogLevel.Fatal;
    }
}
