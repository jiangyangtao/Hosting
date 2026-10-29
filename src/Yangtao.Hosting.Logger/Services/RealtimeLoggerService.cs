using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Threading.Channels;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.Logger.Abstractions;
using Yangtao.Hosting.Logger.Options;

namespace Yangtao.Hosting.Logger.Services
{
    /// <summary>
    /// 实时日志服务
    /// </summary>
    internal class RealtimeLoggerService : IRealtimeLoggerService, IAsyncDisposable
    {
        private readonly ConcurrentDictionary<string, Channel<ILogInfo>> _subscribers = new();
        private readonly Channel<ILogInfo> _centralChannel;
        private readonly RealtimeLoggerOptions _realtimeLoggerOptions;
        private readonly LoggerFormatter _loggerFormatter;
        private readonly Task _pumpTask;
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        public RealtimeLoggerService(IOptions<LoggerOptions> loggerOptions)
        {
            var _loggerOptions = loggerOptions.Value ?? throw new NullReferenceException(nameof(LoggerOptions));
            _realtimeLoggerOptions = _loggerOptions.RealtimeLoggerOptions ?? throw new NullReferenceException(nameof(LoggerOptions.RealtimeLoggerOptions));

            _loggerFormatter = new LoggerFormatter(_realtimeLoggerOptions.Format);
            _centralChannel = Channel.CreateBounded<ILogInfo>(new BoundedChannelOptions(_realtimeLoggerOptions.CentralChannelCapacity)
            {
                FullMode = _realtimeLoggerOptions.CentralChannelFullMode,
                SingleReader = false,
                SingleWriter = false
            });

            _pumpTask = Task.Run(() => PumpAsync(_cancellationTokenSource.Token));
        }

        private async Task PumpAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await foreach (var logInfo in _centralChannel.Reader.ReadAllAsync(cancellationToken))
                {
                    foreach (var subscriber in _subscribers.Values)
                        subscriber.Writer.TryWrite(logInfo); // 非阻塞扇出；慢客户端由自身 Channel 丢弃策略处理
                }
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
            }
        }

        public void Publish(LogInfo logInfo, Exception? exception)
        {
            if (_subscribers.IsEmpty) return;

            logInfo.FormattedText = _loggerFormatter.TextFormat(logInfo, exception);
            _centralChannel.Writer.TryWrite(logInfo);
        }

        public ChannelReader<ILogInfo> Subscribe(string channelKey)
        {
            if (channelKey.IsNullOrEmpty()) throw new ArgumentNullException(nameof(channelKey));

            var exist = _subscribers.TryGetValue(channelKey, out Channel<ILogInfo>? channal);

            if (exist == false || channal == null)
            {
                channal = Channel.CreateBounded<ILogInfo>(new BoundedChannelOptions(_realtimeLoggerOptions.BufferCapacity)
                {
                    FullMode = _realtimeLoggerOptions.ChannelFullMode,
                    SingleReader = true
                });
                _subscribers[channelKey] = channal;
            }
            return channal.Reader;
        }

        public void Unsubscribe(string channelKey)
        {
            if (_subscribers.TryRemove(channelKey, out var channel))
            {
                channel.Writer.TryComplete();
            }
        }

        public async ValueTask DisposeAsync()
        {
            _subscribers.Clear();

            _centralChannel.Writer.TryComplete();
            _cancellationTokenSource.Cancel();

            try
            {
                await _pumpTask.WaitAsync(TimeSpan.FromSeconds(5)).ConfigureAwait(false);
            }
            catch (OperationCanceledException) { }
            catch (TimeoutException)
            {
            }
            finally
            {
                _cancellationTokenSource.Dispose();
            }
        }
    }
}
