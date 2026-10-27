namespace Yangtao.Hosting.Logger.Options
{
    public class LoggerOptions
    {
        /// <summary>
        /// 文件写入日志配置
        /// </summary>
        public FileLogOptions? FileLogOptions { set; get; } = null;

        /// <summary>
        /// 飞书通知配置
        /// </summary>
        public LarkNoticeOptions? LarkNoticeOptions { set; get; } = null;

        /// <summary>
        /// 邮件通知配置
        /// </summary>
        public EmailNoticeOptions? EmailNoticeOptions { set; get; } = null;

        internal void DeepCopy(LoggerOptions loggerOptions)
        {
            if (loggerOptions.FileLogOptions != null)
            {
                FileLogOptions = new FileLogOptions
                {
                    Enable = loggerOptions.FileLogOptions.Enable,
                    Format = loggerOptions.FileLogOptions.Format,
                    MinLevel = loggerOptions.FileLogOptions.MinLevel,
                    FilePath = loggerOptions.FileLogOptions.FilePath,
                    FileName = loggerOptions.FileLogOptions.FileName,
                    BufferSize = loggerOptions.FileLogOptions.BufferSize,
                    FlushIntervalMillisecond = loggerOptions.FileLogOptions.FlushIntervalMillisecond,
                };
            }

            if (loggerOptions.LarkNoticeOptions != null)
            {
                LarkNoticeOptions = new LarkNoticeOptions
                {
                    Enable = loggerOptions.LarkNoticeOptions.Enable,
                    Format = loggerOptions.LarkNoticeOptions.Format,
                    MinLevel = loggerOptions.LarkNoticeOptions.MinLevel,
                    WebhookUrl = loggerOptions.LarkNoticeOptions.WebhookUrl,
                    Secret = loggerOptions.LarkNoticeOptions.Secret,
                };
            }

            if (loggerOptions.EmailNoticeOptions != null)
            {
                EmailNoticeOptions = new EmailNoticeOptions
                {
                    Enable = loggerOptions.EmailNoticeOptions.Enable,
                    Format = loggerOptions.EmailNoticeOptions.Format,
                    MinLevel = loggerOptions.EmailNoticeOptions.MinLevel,
                    EmailName = loggerOptions.EmailNoticeOptions.EmailName,
                    Server = loggerOptions.EmailNoticeOptions.Server,
                    Port = loggerOptions.EmailNoticeOptions.Port,
                    Account = loggerOptions.EmailNoticeOptions.Account,
                    Password = loggerOptions.EmailNoticeOptions.Password,
                };
            }
        }
    }
}
