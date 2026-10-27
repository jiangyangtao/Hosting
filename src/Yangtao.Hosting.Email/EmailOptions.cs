using System.ComponentModel.DataAnnotations;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.Email
{
    public class EmailOptions
    {
        /// <summary>
        /// 发送者名称
        /// </summary>
        [Required]
        public string EmailName { set; get; } = string.Empty;

        [Required]
        public string? Server { set; get; }

        [Range(0, 65535)]
        public int Port { set; get; }

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

        internal virtual void Validate()
        {
            if (EmailName.IsNullOrEmpty()) throw new ArgumentNullException(EmailName, "can not be empty or null.");
            if (Server.IsNullOrEmpty()) throw new ArgumentNullException(Server, "can not be empty or null.");
            if (Port == -1) throw new ArgumentNullException(nameof(Port), "can not be empty or null.");
            if (Account.IsNullOrEmpty()) throw new ArgumentNullException(Account, "can not be empty or null.");
            if (Password.IsNullOrEmpty()) throw new ArgumentNullException(Password, "can not be empty or null.");
        }
    }
}
