using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace Yangtao.Hosting.NLog
{
    /// <summary>
    /// 日志配置基类
    /// </summary>
    public abstract class LogOptionsBase
    {
        /// <summary>
        /// 最小级别
        /// </summary>
        public virtual LogLevel MinLevel { set; get; } = LogLevel.Debug;

        /// <summary>
        /// 最大级别
        /// </summary>
        public virtual LogLevel MaxLevel { set; get; } = LogLevel.Fatal;

        public virtual string LoggerNamePattern { set; get; } = "*";

        /// <summary>
        /// 布局
        /// </summary>
        public virtual Layout? Layout { set; get; } = "${level}: ${longdate} ${newline}      ${callsite}    ${newline}      ${aspnet-request-ip:whenEmpty=localhost} ${newline}      ${message} ${exception:format=tostring}    ${newline}      ${aspnet-request-posted-body}";

        internal virtual LoggingRule BuildLoggingRule(Target target) => new(LoggerNamePattern, MinLevel, MaxLevel, target);
    }
}
