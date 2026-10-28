using Microsoft.Extensions.Logging;

namespace Yangtao.Hosting.Logger.Options
{
    /// <summary>
    /// 日志配置
    /// </summary>
    public class LoggerOptions
    {
        /// <summary>
        /// 启用渠道日志
        /// </summary>
        public bool EnableChannelLogger { set; get; } = false;

        internal bool CanEnableChannel => RealtimeLoggerOptions != null && FileLoggerOptions != null;


        /// <summary>
        /// 启用通知日志
        /// </summary>
        public bool EnableLoggerNotice { set; get; } = false;

        internal bool CanEnableNotice => LarkNoticeOptions != null && EmailNoticeOptions != null;

        /// <summary>
        /// 实时日志配置
        /// </summary>
        public RealtimeLoggerOptions? RealtimeLoggerOptions { set; get; } = null;

        /// <summary>
        /// 文件写入日志配置
        /// </summary>
        public FileLoggerOptions? FileLoggerOptions { set; get; } = null;

        internal bool HasChannelLoggerLevel(LogLevel logLevel)
        {
            if (RealtimeLoggerOptions == null && FileLoggerOptions == null) return false;
            if (RealtimeLoggerOptions == null && FileLoggerOptions != null) return FileLoggerOptions.HasLevel(logLevel);
            if (RealtimeLoggerOptions != null && FileLoggerOptions == null) return RealtimeLoggerOptions.HasLevel(logLevel);
            if (RealtimeLoggerOptions != null && FileLoggerOptions != null)
            {
                return RealtimeLoggerOptions.HasLevel(logLevel) || FileLoggerOptions.HasLevel(logLevel);
            }

            return false;
        }

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
            EnableChannelLogger = loggerOptions.EnableChannelLogger;
            EnableLoggerNotice = loggerOptions.EnableLoggerNotice;


            if (loggerOptions.RealtimeLoggerOptions != null)
            {
                RealtimeLoggerOptions = new RealtimeLoggerOptions
                {
                    Format = loggerOptions.RealtimeLoggerOptions.Format,
                    MinLevel = loggerOptions.RealtimeLoggerOptions.MinLevel,
                    BufferCapacity = loggerOptions.RealtimeLoggerOptions.BufferCapacity,
                    CentralChannelCapacity = loggerOptions.RealtimeLoggerOptions.CentralChannelCapacity,
                    CentralChannelFullMode = loggerOptions.RealtimeLoggerOptions.CentralChannelFullMode,
                    ChannelFullMode = loggerOptions.RealtimeLoggerOptions.ChannelFullMode,
                };
            }
            else loggerOptions.RealtimeLoggerOptions = null;

            if (loggerOptions.FileLoggerOptions != null)
            {
                FileLoggerOptions = new FileLoggerOptions
                {
                    Format = loggerOptions.FileLoggerOptions.Format,
                    MinLevel = loggerOptions.FileLoggerOptions.MinLevel,
                    FilePath = loggerOptions.FileLoggerOptions.FilePath,
                    FileName = loggerOptions.FileLoggerOptions.FileName,
                    BufferCapacity = loggerOptions.FileLoggerOptions.BufferCapacity,
                    FlushIntervalMillisecond = loggerOptions.FileLoggerOptions.FlushIntervalMillisecond,
                    ChannelFullMode = loggerOptions.FileLoggerOptions.ChannelFullMode,
                };
            }
            else loggerOptions.FileLoggerOptions = null;

            if (loggerOptions.LarkNoticeOptions != null)
            {
                LarkNoticeOptions = new LarkNoticeOptions
                {
                    BufferCapacity = loggerOptions.LarkNoticeOptions.BufferCapacity,
                    Format = loggerOptions.LarkNoticeOptions.Format,
                    MinLevel = loggerOptions.LarkNoticeOptions.MinLevel,
                    WebhookUrl = loggerOptions.LarkNoticeOptions.WebhookUrl,
                    Secret = loggerOptions.LarkNoticeOptions.Secret,
                    TitleFormat = loggerOptions.LarkNoticeOptions.TitleFormat,
                };
            }
            else loggerOptions.LarkNoticeOptions = null;

            if (loggerOptions.EmailNoticeOptions != null)
            {
                EmailNoticeOptions = new EmailNoticeOptions
                {
                    BufferCapacity = loggerOptions.EmailNoticeOptions.BufferCapacity,
                    Format = loggerOptions.EmailNoticeOptions.Format,
                    MinLevel = loggerOptions.EmailNoticeOptions.MinLevel,
                    EmailName = loggerOptions.EmailNoticeOptions.EmailName,
                    Server = loggerOptions.EmailNoticeOptions.Server,
                    Port = loggerOptions.EmailNoticeOptions.Port,
                    Account = loggerOptions.EmailNoticeOptions.Account,
                    Password = loggerOptions.EmailNoticeOptions.Password,
                    TitleFormat = loggerOptions.EmailNoticeOptions.TitleFormat,
                };
            }
            else loggerOptions.EmailNoticeOptions = null;
        }
    }
}
