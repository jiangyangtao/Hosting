using Microsoft.Extensions.Logging;
using System.Threading.Channels;

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
        /// 中央日志渠道容量
        /// </summary>
        public int CentralChannelCapacity { set; get; } = 100000;

        /// <summary>
        /// 渠道上限模式
        /// </summary>
        public BoundedChannelFullMode CentralChannelFullMode { set; get; } = BoundedChannelFullMode.DropOldest;

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
            CentralChannelCapacity = loggerOptions.CentralChannelCapacity;
            CentralChannelFullMode = loggerOptions.CentralChannelFullMode;

            if (loggerOptions.RealtimeLoggerOptions != null)
            {
                RealtimeLoggerOptions = new RealtimeLoggerOptions
                {
                    Format = loggerOptions.RealtimeLoggerOptions.Format,
                    MinLevel = loggerOptions.RealtimeLoggerOptions.MinLevel,
                    ChannelCapacity = loggerOptions.RealtimeLoggerOptions.ChannelCapacity,
                };
            }

            if (loggerOptions.FileLoggerOptions != null)
            {
                FileLoggerOptions = new FileLoggerOptions
                {
                    Format = loggerOptions.FileLoggerOptions.Format,
                    MinLevel = loggerOptions.FileLoggerOptions.MinLevel,
                    FilePath = loggerOptions.FileLoggerOptions.FilePath,
                    FileName = loggerOptions.FileLoggerOptions.FileName,
                    BufferSize = loggerOptions.FileLoggerOptions.BufferSize,
                    FlushIntervalMillisecond = loggerOptions.FileLoggerOptions.FlushIntervalMillisecond,
                };
            }

            if (loggerOptions.LarkNoticeOptions != null)
            {
                LarkNoticeOptions = new LarkNoticeOptions
                {
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
