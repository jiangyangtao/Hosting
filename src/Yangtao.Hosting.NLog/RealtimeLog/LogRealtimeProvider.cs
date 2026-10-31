using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System.IO.Pipelines;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Channels;
using Yangtao.Hosting.NLog.Abstractions;

namespace Yangtao.Hosting.NLog.RealtimeLog
{
    internal class LogRealtimeProvider : ILogRealtimeProvider
    {
        private readonly ILogBufferQueueService _logBufferQueueService;
        private static readonly TimeSpan HeartbeatInterval = TimeSpan.FromSeconds(15);

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        private static readonly byte[] ConnectedFrame = Encoding.UTF8.GetBytes(": connected\n\n");
        private static readonly byte[] LogFramePrefix = Encoding.UTF8.GetBytes("event: log\ndata: ");
        private static readonly byte[] FrameSuffix = Encoding.UTF8.GetBytes("\n\n");
        private static readonly byte[] HeartbeatFrame = Encoding.UTF8.GetBytes("event: heartbeat\ndata: alive\n\n");

        public LogRealtimeProvider(ILogBufferQueueService logBufferQueueService)
        {
            _logBufferQueueService = logBufferQueueService;
        }

        public async Task SubscribeAsync(HttpContext context, CancellationToken cancellationToken)
        {
            // 1) 设置 SSE 响应头
            context.Response.StatusCode = StatusCodes.Status200OK;
            context.Response.ContentType = "text/event-stream;charset=utf-8;";
            context.Response.Headers.CacheControl = "no-cache";

            if (HttpProtocol.IsHttp11(context.Request.Protocol))
                context.Response.Headers.Connection = "keep-alive";
            context.Response.Headers["X-Accel-Buffering"] = "no"; // 让 nginx 不要缓冲

            // 2) 关闭响应体缓冲，确保每帧立即到达客户端
            context.Features.Get<IHttpResponseBodyFeature>()?.DisableBuffering();

            // 3) 先发一个初始事件，避免浏览器/代理在首帧前挂起
            await context.Response.WriteAsync("event: connected\ndata: ok\n\n", cancellationToken);
            await context.Response.Body.FlushAsync(cancellationToken);



            using var subscription = _logBufferQueueService.Subscribe(cancellationToken);
            var reader = subscription.Reader;



            // 心跳任务生命周期：linked CTS 跟随请求取消，
            // 同时允许主循环主动停掉它，避免"通道已完成但请求未断"时挂起等待。
            using var heartbeatCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            using var timer = new PeriodicTimer(HeartbeatInterval);

            // StreamWriter 非线程安全，心跳与主循环并发写，用信号量串行化，
            // 保证单个 SSE 帧（event/data/\n\n）不会被拆开。
            var writeGate = new SemaphoreSlim(1, 1);

            try
            {

                // 显式启动响应，确保 headers 在首帧 body 之前发出
                await context.Response.StartAsync(cancellationToken);

                // 连接确认帧（SSE 注释行以 ':' 开头，客户端忽略）
                await WriteFrameAsync(context.Response.BodyWriter, writeGate, ConnectedFrame, cancellationToken);

                // ---------- 并发启动主循环与心跳 ----------
                // 直接调用异步方法，无需 Task.Run：
                //   - 方法体会同步执行到第一个 await（WaitForNextTickAsync）就返回 Task；
                //   - 后续在 IO 完成时由线程池恢复，不会占用线程；
                //   - Task.Run 反而多一次线程调度，对"等待型"异步方法毫无收益。
                var mainTask = StreamLogsAsync(context.Response.BodyWriter, writeGate, subscription.Reader, cancellationToken);
                var heartbeatTask = HeartbeatAsync(context.Response.BodyWriter, writeGate, timer, heartbeatCts.Token);

                try
                {
                    // 等待任一结束：
                    //   - 客户端断开 / 宿主停止 → ct 取消 → 两个任务同时结束；
                    //   - 订阅通道被 Complete（应用关闭）→ mainTask 先结束。
                    await Task.WhenAny(mainTask, heartbeatTask);
                }
                finally
                {
                    // 主动停掉心跳，保证下面收敛时两个任务都能退出；
                    // 对已结束的心跳 Task 调用 Cancel 是无害的。
                    heartbeatCts.Cancel();

                    // 收敛两任务：OCE 是正常路径，被吞掉；
                    // 非 OCE 异常（如 IOException）继续向上传播，交给全局异常处理。
                    try { await mainTask; } catch (OperationCanceledException) { }
                    try { await heartbeatTask; } catch (OperationCanceledException) { }
                }

            }
            catch (OperationCanceledException)
            {
                // 客户端断开
            }
            finally
            {
                writeGate.Dispose();
            }
        }


        /// <summary>
        /// 主循环：从订阅通道流式输出日志帧。
        /// 通道被 Complete（应用关闭）时 ReadAllAsync 优雅结束；
        /// cancellationToken 取消（客户端断开）时抛 OperationCanceledException。
        /// </summary>
        private static async Task StreamLogsAsync(
            PipeWriter writer,
            SemaphoreSlim gate,
            ChannelReader<LogEntry> reader,
            CancellationToken cancellationToken)
        {
            await foreach (var entry in reader.ReadAllAsync(cancellationToken))
            {
                // ① 锁外序列化：心跳不会被序列化拖住，锁竞争窗口缩短到最小
                var jsonBytes = SerializeEntry(entry);

                // ② 锁内只做三件小事：写前缀、写 JSON、写后缀、一次 flush
                await gate.WaitAsync(cancellationToken);
                try
                {
                    await writer.WriteAsync(LogFramePrefix, cancellationToken);
                    await writer.WriteAsync(jsonBytes, cancellationToken);
                    await writer.WriteAsync(FrameSuffix, cancellationToken);
                    await writer.FlushAsync(cancellationToken);   // 整帧一次 flush，不逐段推网络
                }
                finally
                {
                    gate.Release();
                }
            }
        }

        /// <summary>
        /// 序列化 + 兜底。
        /// LogEntry 字段都是简单类型，正常不会失败；极端场景（未来字段扩展后）
        /// 也不让单条日志抛异常打断整条流。
        /// </summary>
        private static byte[] SerializeEntry(LogEntry entry)
        {
            try
            {
                return JsonSerializer.SerializeToUtf8Bytes(entry, JsonOptions);
            }
            catch (Exception ex)
            {
                return JsonSerializer.SerializeToUtf8Bytes(new
                {
                    entry.Timestamp,
                    entry.Level,
                    entry.LoggerName,
                    Message = "[序列化失败] " + entry.Message,
                    Exception = ex.Message
                }, JsonOptions);
            }
        }

        /// <summary>
        /// 心跳循环：定期发送 SSE 事件帧，保持连接不被中间代理超时断开。
        /// 第一个 await 就在 WaitForNextTickAsync 上，不会占用线程。
        /// </summary>
        private static async Task HeartbeatAsync(
            PipeWriter writer,
            SemaphoreSlim gate,
            PeriodicTimer timer,
            CancellationToken ct)
        {
            try
            {
                while (await timer.WaitForNextTickAsync(ct))
                {
                    await WriteFrameAsync(writer, gate, HeartbeatFrame, ct);
                }
            }
            catch (OperationCanceledException)
            {
                // 正常停止
            }
            catch (ObjectDisposedException)
            {
                // 兜底：timer 在释放期被 WaitForNextTickAsync 访问
            }
        }

        /// <summary>
        /// 串行化写入一个完整 SSE 帧并立即 flush。
        /// 所有 await 都透传 ct：取消后写操作能迅速抛出 OCE，而不是卡在 IO 上。
        /// </summary>
        private static async Task WriteFrameAsync(PipeWriter writer, SemaphoreSlim gate, byte[] frame, CancellationToken cancellationToken)
        {
            // WaitAsync 在 try 外：若取消导致其抛 OCE，finally 不会误 Release
            await gate.WaitAsync(cancellationToken);
            try
            {
                await writer.WriteAsync(frame, cancellationToken);
                await writer.FlushAsync(cancellationToken);
            }
            finally
            {
                gate.Release();
            }
        }
    }
}
