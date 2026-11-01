using System.Threading.Channels;
using Yangtao.Hosting.NLog.Abstractions;

namespace Yangtao.Hosting.NLog.RealtimeLog
{
    internal class RealtimeLogSubscription : IRealtimeLogSubscription, IDisposable
    {
        public Guid Id { get; }

        public ChannelReader<LogInfo> Reader { get; }

        private readonly LogBufferQueueService _logBufferQueueService;
        private readonly CancellationTokenRegistration _cancellationTokenRegistration;
        private int _disposed;

        internal RealtimeLogSubscription(
            LogBufferQueueService logBufferQueueService, Guid id, Channel<LogInfo> channel, CancellationToken cancellationToken)
        {
            _logBufferQueueService = logBufferQueueService;
            Id = id;
            Reader = channel.Reader;

            // 兜底：即使 Controller 的 finally 因异常未执行，token 取消也会释放订阅。
            _cancellationTokenRegistration = cancellationToken.Register(
                static state => ((RealtimeLogSubscription)state!).Dispose(), this);

            // 若 cancellationToken 在 Register 时已取消，回调已同步执行 Dispose()，此时 registration
            // 本身也应释放，避免在已取消 token 上留下悬挂的 registration。
            if (Volatile.Read(ref _disposed) != 0) _cancellationTokenRegistration.Dispose();
        }

        public void Dispose()
        {
            // 幂等：using 与 token 回调可能并发触发，只让第一个生效
            if (Interlocked.Exchange(ref _disposed, 1) != 0) return;
            _cancellationTokenRegistration.Dispose();
            _logBufferQueueService.Unsubscribe(Id);
        }
    }
}
