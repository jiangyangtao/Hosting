namespace Yangtao.Hosting.Common
{
    public record SseInfo(Guid Id, string Event, string Data, string Retry);
}
