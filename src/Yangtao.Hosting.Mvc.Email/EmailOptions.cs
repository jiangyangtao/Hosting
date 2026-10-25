using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.Mvc.Email
{
    public class EmailOptions
    {
        /// <summary>
        /// 发送者名称
        /// </summary>
        public string EmailName { set; get; } = string.Empty;

        public string Server { set; get; } = string.Empty;

        public int Port { set; get; } = -1;

        /// <summary>
        /// 发送者账号
        /// </summary>
        public string Account { set; get; } = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { set; get; } = string.Empty;


        internal void Validate()
        {
            if (EmailName.IsNullOrEmpty()) throw new ArgumentNullException(EmailName, "can not be empty or null.");
            if (Server.IsNullOrEmpty()) throw new ArgumentNullException(Server, "can not be empty or null.");
            if (Port == -1) throw new ArgumentNullException(nameof(Port), "can not be empty or null.");
            if (Account.IsNullOrEmpty()) throw new ArgumentNullException(Account, "can not be empty or null.");
            if (Password.IsNullOrEmpty()) throw new ArgumentNullException(Password, "can not be empty or null.");
        }
    }
}
