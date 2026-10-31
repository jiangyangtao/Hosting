namespace Yangtao.Hosting.NLog
{
    public sealed record LogInfo(DateTime Timestamp, string Level, string LoggerName, string Message, string? Exception);
}
