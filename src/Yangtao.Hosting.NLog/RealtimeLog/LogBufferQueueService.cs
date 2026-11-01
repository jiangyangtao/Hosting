using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Threading.Channels;
using Yangtao.Hosting.NLog.Abstractions;

namespace Yangtao.Hosting.NLog.RealtimeLog
{
    internal sealed class LogBufferQueueService : BackgroundService
    {
        private readonly Channel<LogInfo> _mainChannel;

        // 每个 SSE 客户端对应一个订阅通道。客户端断开时从字典移除。
        private readonly ConcurrentDictionary<Guid, Channel<LogInfo>> _subscribers;
        private readonly RealtimeLogOptions _realtimeLogOptions;

        public LogBufferQueueService(IOptions<NLogOptions> options)
        {
            _realtimeLogOptions = options.Value.RealtimeLogOptions ?? throw new NullReferenceException(nameof(NLogOptions.RealtimeLogOptions));

            _mainChannel = Channel.CreateBounded<LogInfo>(
            new BoundedChannelOptions(_realtimeLogOptions.MainChannelCapacity)
            {
                FullMode = _realtimeLogOptions.MainChannelFullMode,
                SingleReader = true,   // 只有 ExecuteAsync 一个读取者
                SingleWriter = false   // 多个业务线程会并发写
            });
            _subscribers = new();
        }

        /// <summary>
        /// 供自定义 NLog Target 调用,非阻塞式，始终快速返回
        /// NLog 的 Write 回调本身是同步的，没有 CancellationToken 链路，
        /// 取消/背压由 Channel 的 DropOldest 策略承担。
        /// </summary>
        public bool TryPublish(LogInfo logInfo) => _mainChannel.Writer.TryWrite(logInfo);

        /// <summary>
        /// 订阅日志流。返回的 IRealtimeLogSubscription 会在 CancellationToken 取消时自动清理，
        /// 调用方用 using 保证正常路径也及时释放。
        /// </summary>
        public IRealtimeLogSubscription Subscribe(CancellationToken cancellationToken)
        {
            var clientId = Guid.NewGuid();

            // 每个客户端独立的有界通道：慢客户端只丢自己的日志，
            // 不会阻塞广播循环，也不会影响其他客户端。
            var channel = Channel.CreateBounded<LogInfo>(
                new BoundedChannelOptions(_realtimeLogOptions.SubscribeChannelCapacity)
                {
                    FullMode = _realtimeLogOptions.SubscribeChannelFullMode,
                    SingleReader = true,
                    SingleWriter = true   // 只有广播循环这一个写入者
                });

            _subscribers[clientId] = channel;
            return new RealtimeLogSubscription(this, clientId, channel, cancellationToken);
        }

        /// <summary>
        /// 内部清理：从字典移除并完成通道，让读取端优雅退出。
        /// </summary>
        /// <param name="clientId"></param>
        public void Unsubscribe(Guid clientId)
        {
            if (_subscribers.TryRemove(clientId, out var channel))
                channel.Writer.TryComplete();
        }

        /// <summary>
        /// 广播循环。stoppingToken 由宿主在应用关闭（SIGTERM/Ctrl+C）时取消；
        /// finally 中无参 TryComplete() 表示正常完成，SSE 端 ReadAllAsync 优雅结束。
        /// </summary>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await foreach (var entry in _mainChannel.Reader.ReadAllAsync(stoppingToken))
                {
                    foreach (var (clientId, channel) in _subscribers)
                    {
                        if (!channel.Writer.TryWrite(entry))
                            _subscribers.TryRemove(clientId, out _);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // 宿主停止，正常退出
            }
            finally
            {
                foreach (var channel in _subscribers.Values)
                    channel.Writer.TryComplete();
            }
        }
    }
}
