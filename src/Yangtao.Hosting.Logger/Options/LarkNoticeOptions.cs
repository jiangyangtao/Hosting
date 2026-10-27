using System.ComponentModel.DataAnnotations;

namespace Yangtao.Hosting.Logger.Options
{
    public class LarkNoticeOptions : LoggerOptionsBase
    {
        [Required]
        public string? WebhookUrl { set; get; }

        public string? Secret { set; get; }
    }
}
