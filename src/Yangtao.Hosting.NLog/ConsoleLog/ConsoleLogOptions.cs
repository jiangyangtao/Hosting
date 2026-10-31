using NLog;
using NLog.Layouts;
using NLog.Targets;

namespace Yangtao.Hosting.NLog.ConsoleLog
{
    public class ConsoleLogOptions : LogOptionsBase
    {
        /// <summary>
        /// 最小级别，默认 Warn
        /// </summary>
        public override LogLevel MinLevel { set; get; } = LogLevel.Debug;

        /// <summary>
        /// 最大级别，默认 Fatal
        /// </summary>
        public override LogLevel MaxLevel { set; get; } = LogLevel.Fatal;

        public override Layout? Layout { set; get; } = null;

        public bool UseDefaultRowHighlightingRules { set; get; } = false;

        public IEnumerable<ConsoleRowHighlightingRule> RowHighlightingRules { set; get; } = Array.Empty<ConsoleRowHighlightingRule>();

        public IEnumerable<ConsoleWordHighlightingRule> WordHighlightingRules { set; get; } = Array.Empty<ConsoleWordHighlightingRule>();
    }
}
