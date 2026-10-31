using System.ComponentModel.DataAnnotations;

namespace Yangtao.Hosting.NLog.NoticeLog
{
    public class LarkNoticeOptions : NoticeOptionsBase
    {
        [Required]
        public string? WebhookUrl { set; get; }

        public string? Secret { set; get; }
    }
}
