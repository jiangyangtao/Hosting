using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using System.IO.Pipelines;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Channels;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.NLog.Abstractions;

namespace Yangtao.Hosting.NLog.RealtimeLog
{
    internal class RealtimeLogProvider : IRealtimeLogProvider
    {
        private readonly LogBufferQueueService _logBufferQueueService;
        private readonly RealtimeLogOptions _realtimeLogOptions;
        private readonly TimeSpan _heartbeatInterval;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping // 关闭非 ASCII 转义
        };

        public RealtimeLogProvider(LogBufferQueueService logBufferQueueService, IOptions<NLogOptions> options)
        {
            _logBufferQueueService = logBufferQueueService;
            _realtimeLogOptions = options.Value.RealtimeLogOptions ?? throw new NullReferenceException(nameof(NLogOptions.RealtimeLogOptions));

            _heartbeatInterval = TimeSpan.FromSeconds(_realtimeLogOptions.HeartbeatInterval);
        }

        public async Task SubscribeAsync(HttpContext context, CancellationToken cancellationToken)
        {
            // 1) 设置 SSE 响应头
            context.Response.StatusCode = StatusCodes.Status200OK;
            context.Response.ContentType = "text/event-stream;charset=utf-8;";
            context.Response.Headers.CacheControl = "no-cache";

            if (HttpProtocol.IsHttp11(context.Request.Protocol))
                context.Response.Headers.Connection = "keep-alive";
            context.Response.Headers["X-Accel-Buffering"] = "no"; // 禁止 nginx 缓冲

            // 2) 关闭响应体缓冲，确保每个数据帧立即到达客户端
            context.Features.Get<IHttpResponseBodyFeature>()?.DisableBuffering();

            // 3) 发送初始事件，避免浏览器/代理在连接后挂起
            await context.Response.Body.WriteAsync(ConnectedBytes, cancellationToken);
            await context.Response.Body.FlushAsync(cancellationToken);


            using var subscription = _logBufferQueueService.Subscribe(cancellationToken);

            // 心跳任务生命周期：linked CancellationTokenSource 跟随请求取消，
            // 同时允许主循环主动停掉它，避免"通道已完成但请求未断"时挂起等待。
            using var heartbeatCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            using var timer = new PeriodicTimer(_heartbeatInterval);

            // 信号量串行化，保证单个 SSE 数据帧不会被拆开
            var writeGate = new SemaphoreSlim(1, 1);

            try
            {
                // 显式启动响应，确保 headers 在首帧 body 之前发出
                await context.Response.StartAsync(cancellationToken);

                // 连接确认帧（SSE 注释行以 ':' 开头，客户端忽略）
                //await WriteFrameStreamAsync(context.Response.BodyWriter, writeGate, ConnectedBytes, cancellationToken);

                // 异步流任务
                var mainTask = WriteLogStreamAsync(context.Response.BodyWriter, writeGate, subscription.Reader, cancellationToken);

                // 心跳任务
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
                    // 主动停掉心跳任务，保证下面收敛时两个任务都能退出；
                    // 对已结束的心跳任务调用 Cancel 无影响
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
        /// 连接数据流
        /// </summary>
        private byte[] ConnectedBytes
        {
            get
            {
                if (_realtimeLogOptions.LorResponseType == LorResponseType.Text) return Encoding.UTF8.GetBytes("event: connected\ndata: ok\n\n");

                return Encoding.UTF8.GetBytes("{\"event\": \"connected\",\"data\":\"ok\"}");
            }
        }

        /// <summary>
        /// 心跳数据流
        /// </summary>
        private byte[] HeartbeatBytes
        {
            get
            {
                if (_realtimeLogOptions.LorResponseType == LorResponseType.Text) return Encoding.UTF8.GetBytes("event: heartbeat\ndata: keep-alive\n\n");

                return Encoding.UTF8.GetBytes("{\"event\": \"heartbeat\",\"data\": \"keep-alive\"}");
            }
        }

        /// <summary>
        /// 日志前辍数据流
        /// </summary>
        private byte[] LogPrefixBytes
        {
            get
            {
                if (_realtimeLogOptions.LorResponseType == LorResponseType.Text) return Encoding.UTF8.GetBytes("event: log\ndata: ");

                return Encoding.UTF8.GetBytes("{\"event\": \"log\",\"data\": ");
            }
        }

        /// <summary>
        /// 日志后辍数据流
        /// </summary>
        private byte[] LogSuffixBytes
        {
            get
            {
                if (_realtimeLogOptions.LorResponseType == LorResponseType.Text) return Encoding.UTF8.GetBytes("\n\n");

                return Encoding.UTF8.GetBytes("}");
            }
        }



        /// <summary>
        /// 主循环：从订阅通道流式输出日志
        /// 通道被 Complete（应用关闭）时 ReadAllAsync 优雅结束；
        /// cancellationToken 取消（客户端断开）时抛 OperationCanceledException。
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="gate"></param>
        /// <param name="reader"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task WriteLogStreamAsync(
            PipeWriter writer,
            SemaphoreSlim gate,
            ChannelReader<LogInfo> reader,
            CancellationToken cancellationToken)
        {
            await foreach (var logInfo in reader.ReadAllAsync(cancellationToken))
            {
                // 锁外序列化：心跳不会被序列化拖住，锁竞争窗口缩短到最小
                var logBytes = BuildLogBytes(logInfo);

                // 锁内只做三件小事：先写前缀， 再写日志，最后写后缀，一次 flush
                await gate.WaitAsync(cancellationToken);
                try
                {
                    await writer.WriteAsync(LogPrefixBytes, cancellationToken);
                    await writer.WriteAsync(logBytes, cancellationToken);
                    await writer.WriteAsync(LogSuffixBytes, cancellationToken);
                    await writer.FlushAsync(cancellationToken);   // 一次 flush，不逐段推
                }
                finally
                {
                    gate.Release();
                }
            }
        }

        /// <summary>
        /// 序列化 + 兜底
        /// 保证单条日志抛异常不会中断输出
        /// </summary>
        private byte[] BuildLogBytes(LogInfo logInfo)
        {
            try
            {
                if (_realtimeLogOptions.LorResponseType == LorResponseType.Text) return Encoding.UTF8.GetBytes(logInfo.Message);

                return JsonSerializer.SerializeToUtf8Bytes(logInfo, JsonOptions);
            }
            catch (Exception ex)
            {
                var innerException = ex.GetInnerException();
                if (_realtimeLogOptions.LorResponseType == LorResponseType.Text) return Encoding.UTF8.GetBytes(innerException.Message);

                return JsonSerializer.SerializeToUtf8Bytes(new
                {
                    logInfo.Timestamp,
                    logInfo.Level,
                    logInfo.LoggerName,
                    Message = $"[SerializeFailed]{logInfo.Message}",
                    Exception = innerException.Message,
                }, JsonOptions);
            }
        }




        /// <summary>
        /// 心跳循环：定期发送 SSE 数据帧，保持连接不被中间代理超时断开。
        /// 第一个 await 在 WaitForNextTickAsync 上，不占用线程
        /// </summary>
        private async Task HeartbeatAsync(
            PipeWriter writer,
            SemaphoreSlim gate,
            PeriodicTimer timer,
            CancellationToken cancellationToken)
        {
            try
            {
                // 循环等待触发
                while (await timer.WaitForNextTickAsync(cancellationToken))
                {
                    await WriteFrameStreamAsync(writer, gate, HeartbeatBytes, cancellationToken);
                }
            }
            catch (Exception ex) when (ex is OperationCanceledException or ObjectDisposedException)
            {
                // 正常停止
                // 设计保留，当前不会触发
            }
        }

        /// <summary>
        /// 输出非日志数据流
        /// 串行化写入一个完整 SSE 帧并立即 flush。
        /// 所有 await 都透传 cancellationToken：取消后写操作能迅速抛出 OperationCanceledException，不会卡在 IO 上
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="gate"></param>
        /// <param name="frame"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static async Task WriteFrameStreamAsync(PipeWriter writer, SemaphoreSlim gate, byte[] frame, CancellationToken cancellationToken)
        {
            // WaitAsync 在 try 外：若取消会抛 OperationCanceledException，finally 保证信号量 Release
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
