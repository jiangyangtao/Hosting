using NLog;
using System.ComponentModel.DataAnnotations;
using Yangtao.Hosting.Email;

namespace Yangtao.Hosting.NLog.NoticeLog
{
    public class EmailNoticeOptions : NoticeOptionsBase
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

        public override LogLevel MinLevel { set; get; } = LogLevel.Warn;

        public override LogLevel MaxLevel { set; get; } = LogLevel.Fatal;

        internal EmailOptions EmailOptions => new()
        {
            EmailName = EmailName,
            Server = Server,
            Port = Port,
            Account = Account,
            Password = Password
        };
    }
}
