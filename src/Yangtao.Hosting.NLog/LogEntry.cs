namespace Yangtao.Hosting.NLog
{
    public sealed record LogEntry(
      DateTime Timestamp,
      string Level,
      string LoggerName,
      string Message,
      string? Exception);
}
