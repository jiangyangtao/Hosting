using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.Logger.Abstractions;

namespace Yangtao.Hosting.Logger
{
    internal class LoggerFormatter
    {
        private readonly string _textTemplate;
        private readonly string? _titleTemplate;

        public LoggerFormatter(string textTemplate)
        {
            _textTemplate = textTemplate;
        }

        public LoggerFormatter(string textTemplate, string titleTemplate)
        {
            _textTemplate = textTemplate;
            _titleTemplate = titleTemplate;
        }

        public string TextFormat(ILogInfo log, Exception? exception)
        {
            var text = _textTemplate
                .Replace("{Timestamp:HH:mm:ss}", log.Timestamp.ToString("HH:mm:ss"))
                .Replace("{Timestamp:yyyy-MM-dd HH:mm:ss.fff}", log.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                .Replace("{Level}", log.Level.ToString())
                .Replace("{Category}", log.Category)
                .Replace("{Message}", log.Message);

            if (exception != null)
                text += Environment.NewLine + exception;

            return text;
        }

        public string TitleFormat(ILogInfo log)
        {
            if (_titleTemplate.IsNullOrEmpty()) return string.Empty;

            var text = _titleTemplate
                .Replace("{Timestamp:HH:mm:ss}", log.Timestamp.ToString("HH:mm:ss"))
                .Replace("{Timestamp:yyyy-MM-dd HH:mm:ss.fff}", log.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                .Replace("{Level}", log.Level.ToString())
                .Replace("{Category}", log.Category)
                .Replace("{Message}", log.Message);

            return text;
        }
    }
}
