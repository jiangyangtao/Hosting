using NLog;
using NLog.Layouts;

namespace Yangtao.Hosting.NLog
{
    public abstract class LogOptionsBase
    {
        /// <summary>
        /// 最小级别
        /// </summary>
        public LogLevel MinLevel { set; get; } = LogLevel.Debug;

        /// <summary>
        /// 最大级别
        /// </summary>
        public LogLevel MaxLevel { set; get; } = LogLevel.Fatal;

        public string LoggerNamePattern { set; get; } = "*";

        /// <summary>
        /// 布局
        /// </summary>
        public Layout Layout { set; get; } = "${level}: ${longdate} ${newline}      ${callsite}    ${newline}      ${aspnet-request-ip:whenEmpty=localhost} ${newline}      ${message} ${exception:format=tostring}    ${newline}      ${aspnet-request-posted-body}";
    }
}
