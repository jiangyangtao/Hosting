using System.ComponentModel.DataAnnotations;

namespace Yangtao.Hosting.Logger.Options
{
    public class EmailNoticeOptions : LoggerOptionsBase
    {
        /// <summary>
        /// 发送者名称
        /// </summary>
        [Required]
        public string? EmailName { set; get; }

        [Required]
        public string? Server { set; get; }

        [Range(0, 65535)]
        public int Port { set; get; } = -1;

        /// <summary>
        /// 发送者账号
        /// </summary>
        [Required]
        public string? Account { set; get; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string? Password { set; get; }
    }
}
