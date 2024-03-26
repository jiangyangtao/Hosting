using NLog;

namespace Yangtao.Hosting.NLog
{
    public class NLogConfigurationOptions
    {
        internal NLogConfigurationOptions(){ }

        public FileLayoutType FileLayoutType { set; get; } = FileLayoutType.Custom;

        public LogLevel ConsoleLogLevel { set; get; } = LogLevel.Info;

        public LogLevel FileLogLevel { set; get; } = LogLevel.Error;
    }
}
