using System.ComponentModel.DataAnnotations;
using Yangtao.Hosting.NLog.ConsoleLog;
using Yangtao.Hosting.NLog.FileLog;
using Yangtao.Hosting.NLog.NoticeLog;
using Yangtao.Hosting.NLog.RealtimeLog;

namespace Yangtao.Hosting.NLog
{
    /// <summary>
    /// nlog 配置
    /// </summary>
    public class NLogOptions
    {

        /// <summary>
        /// 服务名
        /// </summary>
        [Required]
        public string ServiceName { set; get; } = string.Empty;

        /// <summary>
        /// 实时日志，为 null 则不开启
        /// 默认开启
        /// </summary>
        public RealtimeLogOptions? RealtimeLogOptions { set; get; } = new RealtimeLogOptions();

        /// <summary>
        /// 文件日志，为 null 则不开启
        /// 默认开启
        /// </summary>
        public FileLogOptions FileLogOptions { set; get; } = new FileLogOptions();

        /// <summary>
        /// 控制台日志，为 null 则不开启
        /// 默认开启
        /// </summary>
        public ConsoleLogOptions ConsoleLogOptions { set; get; } = new ConsoleLogOptions();

        /// <summary>
        /// 飞书通知日志，为 null 则不开启
        /// 默认不开启
        /// </summary>
        public LarkNoticeOptions? LarkNoticeOptions { set; get; } = null;

        /// <summary>
        /// 邮件通知日志，为 null 则不开启
        /// 默认不开启
        /// </summary>
        public EmailNoticeOptions? EmailNoticeOptions { set; get; } = null;

        public bool EnableAsyncNoticeLog { set; get; } = false;

        internal void DeepCopy(NLogOptions options)
        {
            ServiceName = options.ServiceName;
            EnableAsyncNoticeLog = options.EnableAsyncNoticeLog;

            if (options.RealtimeLogOptions != null)
            {
                RealtimeLogOptions = new RealtimeLogOptions
                {
                    Layout = options.RealtimeLogOptions.Layout,
                    MinLevel = options.RealtimeLogOptions.MinLevel,
                    MaxLevel = options.RealtimeLogOptions.MaxLevel,
                    LoggerNamePattern = options.RealtimeLogOptions.LoggerNamePattern,
                    HeartbeatInterval = options.RealtimeLogOptions.HeartbeatInterval,
                    MainChannelCapacity = options.RealtimeLogOptions.MainChannelCapacity,
                    MainChannelFullMode = options.RealtimeLogOptions.MainChannelFullMode,
                    SubscribeChannelCapacity = options.RealtimeLogOptions.SubscribeChannelCapacity,
                    SubscribeChannelFullMode = options.RealtimeLogOptions.SubscribeChannelFullMode,
                    LorResponseType = options.RealtimeLogOptions.LorResponseType
                };
            }
            else RealtimeLogOptions = null;

            if (options.FileLogOptions != null)
            {
                FileLogOptions = new FileLogOptions
                {
                    Layout = options.FileLogOptions.Layout,
                    MinLevel = options.FileLogOptions.MinLevel,
                    MaxLevel = options.FileLogOptions.MaxLevel,
                    LoggerNamePattern = options.FileLogOptions.LoggerNamePattern,
                    FileName = options.FileLogOptions.FileName,
                    LorResponseType = options.FileLogOptions.LorResponseType,
                };
            }
            else FileLogOptions = null;

            if (options.LarkNoticeOptions != null)
            {
                LarkNoticeOptions = new LarkNoticeOptions
                {
                    Layout = options.LarkNoticeOptions.Layout,
                    MinLevel = options.LarkNoticeOptions.MinLevel,
                    MaxLevel = options.LarkNoticeOptions.MaxLevel,
                    LoggerNamePattern = options.LarkNoticeOptions.LoggerNamePattern,
                    WebhookUrl = options.LarkNoticeOptions.WebhookUrl,
                    Secret = options.LarkNoticeOptions.Secret,
                    AfterAsyncDelay = options.LarkNoticeOptions.AfterAsyncDelay,
                    Concurrency = options.LarkNoticeOptions.Concurrency,
                    QueueCapacity = options.LarkNoticeOptions.QueueCapacity,
                };
            }
            else LarkNoticeOptions = null;

            if (options.EmailNoticeOptions != null)
            {
                EmailNoticeOptions = new EmailNoticeOptions
                {
                    Layout = options.EmailNoticeOptions.Layout,
                    MinLevel = options.EmailNoticeOptions.MinLevel,
                    MaxLevel = options.EmailNoticeOptions.MaxLevel,
                    LoggerNamePattern = options.EmailNoticeOptions.LoggerNamePattern,
                    EmailName = options.EmailNoticeOptions.EmailName,
                    Server = options.EmailNoticeOptions.Server,
                    Port = options.EmailNoticeOptions.Port,
                    Account = options.EmailNoticeOptions.Account,
                    Password = options.EmailNoticeOptions.Password,
                    AfterAsyncDelay = options.EmailNoticeOptions.AfterAsyncDelay,
                    Concurrency = options.EmailNoticeOptions.Concurrency,
                    QueueCapacity = options.EmailNoticeOptions.QueueCapacity,
                };
            }
            else EmailNoticeOptions = null;
        }
    }
}
