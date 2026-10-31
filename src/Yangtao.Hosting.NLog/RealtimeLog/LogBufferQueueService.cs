using Microsoft.Extensions.Hosting;
using System.Collections.Concurrent;
using System.Threading.Channels;
using Yangtao.Hosting.NLog.Abstractions;

namespace Yangtao.Hosting.NLog.RealtimeLog
{
    internal sealed class LogBufferQueueService : BackgroundService, ILogBufferQueueService
    {
        // 中心通道：容量 5000，满时丢弃最旧条目。
        // DropOldest 下 TryWrite 不会阻塞，NLog 写路径始终保持快速返回。
        private readonly Channel<LogEntry> _inbound;

        // 每个 SSE 客户端对应一个订阅通道。客户端断开时从字典移除。
        private readonly ConcurrentDictionary<Guid, Channel<LogEntry>> _subscribers;

        public LogBufferQueueService()
        {
            _inbound = Channel.CreateBounded<LogEntry>(
            new BoundedChannelOptions(5000)
            {
                FullMode = BoundedChannelFullMode.DropOldest,
                SingleReader = true,   // 只有 ExecuteAsync 一个读取者
                SingleWriter = false   // 多个业务线程会并发写
            });
            _subscribers = new();
        }

        /// <summary>
        /// 供自定义 NLog Target 调用。非阻塞、始终快速返回。
        /// NLog 的 Write 回调本身是同步的，没有 CancellationToken 概念，
        /// 取消/背压由 Channel 的 DropOldest 策略承担。
        /// </summary>
        public bool TryPublish(LogEntry entry) => _inbound.Writer.TryWrite(entry);

        /// <summary>
        /// 订阅日志流。返回的 LogSubscription 会在 ct 取消时自动清理，
        /// 调用方用 using 保证正常路径也及时释放。
        /// </summary>
        public ILogSubscription Subscribe(CancellationToken ct)
        {
            var clientId = Guid.NewGuid();

            // 每个客户端独立的有界通道：慢客户端只丢自己的日志，
            // 不会阻塞广播循环，也不会影响其他客户端。
            var channel = Channel.CreateBounded<LogEntry>(
                new BoundedChannelOptions(1000)
                {
                    FullMode = BoundedChannelFullMode.DropOldest,
                    SingleReader = true,
                    SingleWriter = true   // 只有广播循环这一个写入者
                });

            _subscribers[clientId] = channel;
            return new LogSubscription(this, clientId, channel, ct);
        }

        /// <summary>内部清理：从字典移除并完成通道，让读取端优雅退出。</summary>
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
                await foreach (var entry in _inbound.Reader.ReadAllAsync(stoppingToken))
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
