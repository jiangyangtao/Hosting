using System.Threading.Channels;

namespace Yangtao.Hosting.NLog.Abstractions
{
    public interface ILogSubscription : IDisposable
    {
        Guid Id { get; }

        ChannelReader<LogEntry> Reader { get; }
    }
}
