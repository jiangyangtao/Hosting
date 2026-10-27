using Microsoft.Extensions.Logging;

namespace Yangtao.Hosting.Logger.Options
{
    public abstract class LoggerOptionsBase
    {
        /// <summary>
        /// 格式
        /// </summary>
        public string? Format { set; get; }

        /// <summary>
        /// 是否启用。
        /// true 启用，false 禁用
        /// </summary>
        public bool Enable { set; get; } = true;

        /// <summary>
        /// 最低日志级别(含)
        /// </summary>
        public LogLevel MinLevel { set; get; } = LogLevel.Information;
    }
}
