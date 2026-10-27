using Microsoft.Extensions.Options;
using MimeKit;

namespace Yangtao.Hosting.Email
{
    public class EmailMessage : EmailOptions
    {
        public EmailMessage()
        {
            Messages = new();
        }

        public EmailMessage(EmailOptions emailOptions) : this()
        {
            EmailName = emailOptions.EmailName;
            Server = emailOptions.Server;
            Port = emailOptions.Port;
            Account = emailOptions.Account;
            Password = emailOptions.Password;
        }

        /// <summary>
        /// 消息。
        /// 总数不能大于10000条
        /// </summary>
        public List<MimeMessage> Messages { get; }

        internal override void Validate()
        {
            base.Validate();

            if (Messages.Count > 10000) throw new ArgumentOutOfRangeException(nameof(Messages), "The total cannot exceed 10000.");
        }
    }
}
