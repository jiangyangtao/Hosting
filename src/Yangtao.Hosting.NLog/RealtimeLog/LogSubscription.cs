using System.Threading.Channels;
using Yangtao.Hosting.NLog.Abstractions;

namespace Yangtao.Hosting.NLog.RealtimeLog
{
    internal class LogSubscription : ILogSubscription, IDisposable
    {
        public Guid Id { get; }

        public ChannelReader<LogEntry> Reader { get; }

        private readonly LogBufferQueueService _logBufferQueueService;
        private readonly CancellationTokenRegistration _ctRegistration;
        private int _disposed;

        internal LogSubscription(
            LogBufferQueueService logBufferQueueService, Guid id, Channel<LogEntry> channel, CancellationToken ct)
        {
            _logBufferQueueService = logBufferQueueService;
            Id = id;
            Reader = channel.Reader;

            // 兜底：即使 Controller 的 finally 因异常未执行，token 取消也会释放订阅。
            _ctRegistration = ct.Register(
                static state => ((LogSubscription)state!).Dispose(), this);

            // 若 ct 在 Register 时已取消，回调已同步执行 Dispose()，此时 registration
            // 本身也应释放，避免在已取消 token 上留下悬挂的 registration。
            if (Volatile.Read(ref _disposed) != 0) _ctRegistration.Dispose();
        }

        public void Dispose()
        {
            // 幂等：using 与 token 回调可能并发触发，只让第一个生效
            if (Interlocked.Exchange(ref _disposed, 1) != 0) return;
            _ctRegistration.Dispose();
            _logBufferQueueService.Unsubscribe(Id);
        }
    }
}
