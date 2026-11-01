using System.Threading.Channels;

namespace Yangtao.Hosting.NLog.Abstractions
{
    /// <summary>
    /// 实时日志订阅
    /// </summary>
    public interface IRealtimeLogSubscription : IDisposable
    {
        /// <summary>
        /// 订阅通道 Id
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// 日志流通道
        /// </summary>
        ChannelReader<LogInfo> Reader { get; }
    }
}
