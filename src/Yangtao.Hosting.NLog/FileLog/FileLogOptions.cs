using NLog;
using NLog.Layouts;

namespace Yangtao.Hosting.NLog.FileLog
{
    public class FileLogOptions : LogOptionsBase
    {
        /// <summary>
        /// 文件名
        /// 含路径
        /// </summary>
        public string? FileName { set; get; } = FileLogBuilder.DefaultFileName;

        /// <summary>
        /// 最小级别，默认 Warn
        /// </summary>
        public override LogLevel MinLevel { set; get; } = LogLevel.Warn;

        /// <summary>
        /// 最大级别，默认 Fatal
        /// </summary>
        public override LogLevel MaxLevel { set; get; } = LogLevel.Fatal;

        public override Layout? Layout { set; get; } = null;

        public LorResponseType LorResponseType { set; get; } = LorResponseType.Text;

        internal Layout FileLayout
        {
            get
            {
                if (Layout != null) return Layout;

                if (LorResponseType == LorResponseType.Json) return NLogBuilder.BuildJsonLayout();

                return FileLogBuilder.DefaultFileLogLayout;
            }
        }
    }
}
