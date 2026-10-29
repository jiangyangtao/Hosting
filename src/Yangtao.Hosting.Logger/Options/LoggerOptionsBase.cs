using Microsoft.Extensions.Logging;

namespace Yangtao.Hosting.Logger.Options
{
    public abstract class LoggerOptionsBase
    {
        /// <summary>
        /// 格式
        /// </summary>
        public string Format { set; get; } = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Category} → {Message}";

        /// <summary>
        /// 最低日志级别(含)
        /// </summary>
        public virtual LogLevel MinLevel { set; get; } = LogLevel.Information;

        public virtual int BufferCapacity { set; get; } = 1000;

        internal bool HasLevel(LogLevel logLevel)
        {
            if (logLevel == LogLevel.None) return false;

            return logLevel >= MinLevel;
        }
    }
}
