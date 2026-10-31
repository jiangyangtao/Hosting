using System.Threading.Channels;

namespace Yangtao.Hosting.NLog.Abstractions
{
    public interface IRealtimeLogSubscription : IDisposable
    {
        Guid Id { get; }

        ChannelReader<LogInfo> Reader { get; }
    }
}
