namespace Yangtao.Hosting.Logger
{
    internal class LoggerFormatter
    {
        private readonly string _template;

        public LoggerFormatter(string template)
        {
            _template = template;
        }

        public string Format(LogInfo log, Exception? exception)
        {
            var text = _template
                .Replace("{Timestamp:HH:mm:ss}", log.Timestamp.ToString("HH:mm:ss"))
                .Replace("{Timestamp:yyyy-MM-dd HH:mm:ss.fff}", log.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                .Replace("{Level}", log.Level.ToString())
                .Replace("{Category}", log.Category)
                .Replace("{Message}", log.Message);

            if (exception != null)
                text += Environment.NewLine + exception;

            return text;
        }
    }
}
