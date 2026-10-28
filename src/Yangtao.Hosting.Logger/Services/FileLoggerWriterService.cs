using Microsoft.Extensions.Options;
using System.Threading.Channels;
using Yangtao.Hosting.Logger.Options;

namespace Yangtao.Hosting.Logger.Services
{
    /// <summary>
    /// 文件日志服务
    /// </summary>
    internal class FileLoggerWriterService : IAsyncDisposable
    {
        private readonly Channel<LogInfo> _channel;
        private readonly FileLoggerOptions _options;
        private readonly Task _pumpTask;
        private readonly CancellationTokenSource _cts = new();
        private readonly Dictionary<string, (StreamWriter Writer, DateTime LastWrite)> _writers = new();
        private int _disposed;
        private readonly LoggerFormatter _loggerFormatter;

        public FileLoggerWriterService(IOptions<FileLoggerOptions> options)
        {
            _options = options.Value;
            _channel = Channel.CreateBounded<LogInfo>(new BoundedChannelOptions(_options.BufferSize)
            {
                FullMode = BoundedChannelFullMode.DropOldest, // 磁盘慢时丢旧日志，绝不阻塞
                SingleReader = true,
                SingleWriter = false
            });
            _loggerFormatter = new LoggerFormatter(_options.Format);
            _pumpTask = Task.Run(PumpAsync);
        }

        public void Write(LogInfo logInfo, Exception? exception)
        {
            logInfo.FormattedText = _loggerFormatter.Format(logInfo, exception);
            _channel.Writer.TryWrite(logInfo);
        }

        private async Task PumpAsync()
        {
            var buffer = new List<LogInfo>(256);
            var flushInterval = TimeSpan.FromMilliseconds(_options.FlushIntervalMillisecond);
            var lastFlush = DateTime.UtcNow;

            try
            {
                while (await _channel.Reader.WaitToReadAsync(_cts.Token))
                {
                    buffer.Clear();
                    while (buffer.Count < 256 && _channel.Reader.TryRead(out var item))
                        buffer.Add(item);

                    if (buffer.Count > 0)
                        await WriteBatchAsync(buffer);

                    if (DateTime.UtcNow - lastFlush >= flushInterval)
                    {
                        await FlushAllAsync();
                        await CleanupStaleWritersAsync();
                        lastFlush = DateTime.UtcNow;
                    }
                }
            }
            catch (OperationCanceledException) { }
            finally
            {
                // 关闭前把 channel 里剩余的全部冲刷
                var remaining = new List<LogInfo>();
                while (_channel.Reader.TryRead(out var item)) remaining.Add(item);
                if (remaining.Count > 0) await WriteBatchAsync(remaining);
                await CloseAllAsync();
            }
        }

        private async Task WriteBatchAsync(List<LogInfo> batch)
        {
            foreach (var item in batch)
            {
                var path = ResolvePath(_options.FilePath, item);
                if (!_writers.TryGetValue(path, out var slot))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                    var stream = new FileStream(path, FileMode.Append, FileAccess.Write,
                        FileShare.Read, 4096, useAsync: true);
                    slot = (new StreamWriter(stream) { AutoFlush = false }, DateTime.UtcNow);
                }
                await slot.Writer.WriteLineAsync(item.FormattedText);
                _writers[path] = (slot.Writer, DateTime.UtcNow);
            }
        }

        private async Task FlushAllAsync()
        {
            foreach (var (writer, _) in _writers.Values)
                await writer.FlushAsync();
        }

        private async Task CleanupStaleWritersAsync()
        {
            // 关闭 1 分钟未写入的 writer（例如按日期滚动后旧文件）
            var cutoff = DateTime.UtcNow.AddMinutes(-1);
            var stale = _writers.Where(kv => kv.Value.LastWrite < cutoff).Select(kv => kv.Key).ToList();
            foreach (var key in stale)
            {
                var (writer, _) = _writers[key];
                await writer.FlushAsync();
                await writer.DisposeAsync();
                _writers.Remove(key);
            }
        }

        private async Task CloseAllAsync()
        {
            foreach (var (writer, _) in _writers.Values)
            {
                await writer.FlushAsync();
                await writer.DisposeAsync();
            }
            _writers.Clear();
        }

        private static string ResolvePath(string template, LogInfo logInfo)
        {
            var path = template
                .Replace("{Date}", logInfo.Timestamp.ToString("yyyyMMdd"))
                .Replace("{Hour}", logInfo.Timestamp.ToString("yyyyMMdd-HH"))
                .Replace("{Level}", logInfo.Level.ToString());
            return Path.IsPathRooted(path) ? path : Path.Combine(AppContext.BaseDirectory, path);
        }

        public async ValueTask DisposeAsync()
        {
            if (Interlocked.Exchange(ref _disposed, 1) == 1) return;
            _channel.Writer.TryComplete();
            _cts.Cancel();
            try { await _pumpTask; } catch (OperationCanceledException) { }
            _cts.Dispose();
        }
    }
}
