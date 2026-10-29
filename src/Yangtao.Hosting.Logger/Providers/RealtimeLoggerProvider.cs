using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Yangtao.Hosting.Logger.Abstractions;

namespace Yangtao.Hosting.Logger.Providers
{
    internal class RealtimeLoggerProvider : IRealtimeLoggerProvider
    {
        private readonly IRealtimeLoggerService _realtimeLoggerService;

        public RealtimeLoggerProvider(IRealtimeLoggerService realtimeLoggerService)
        {
            _realtimeLoggerService = realtimeLoggerService;
        }

        public async Task SubscribeAsync(HttpContext context, CancellationToken cancellationToken)
        {
            // 1) 设置 SSE 响应头
            context.Response.ContentType = "text/event-stream";
            context.Response.Headers.CacheControl = "no-cache";
            context.Response.Headers.Connection = "keep-alive";
            context.Response.Headers["X-Accel-Buffering"] = "no"; // 让 nginx 不要缓冲

            // 2) 关闭响应体缓冲，确保每帧立即到达客户端
            context.Features.Get<IHttpResponseBodyFeature>()?.DisableBuffering();

            // 3) 先发一个初始事件，避免浏览器/代理在首帧前挂起
            await context.Response.WriteAsync("event: connected\ndata: ok\n\n", cancellationToken);
            await context.Response.Body.FlushAsync(cancellationToken);


            var channelKey = "RealtimeLogger";
            var reader = _realtimeLoggerService.Subscribe(channelKey);


            try
            {
                //await foreach (var entry in reader.ReadAllAsync(cancellationToken))
                //{
                //    await Response.WriteAsync($"data: {entry.FormattedText}\n\n", cancellationToken);
                //    await Response.Body.FlushAsync(cancellationToken);
                //}

                while (!cancellationToken.IsCancellationRequested)
                {
                    var waitTask = reader.WaitToReadAsync(cancellationToken).AsTask();
                    var delayTask = Task.Delay(TimeSpan.FromSeconds(15), cancellationToken);

                    var completed = await Task.WhenAny(waitTask, delayTask);
                    if (completed == delayTask)
                    {
                        // 发送心跳注释行
                        await context.Response.WriteAsync(": keep-alive\n\n", cancellationToken);
                        await context.Response.Body.FlushAsync(cancellationToken);
                        continue;
                    }

                    if (!await waitTask) break;

                    while (reader.TryRead(out var log))
                    {
                        await context.Response.WriteAsync($"data: {log.FormattedText}\n\n", cancellationToken);
                    }
                    await context.Response.Body.FlushAsync(cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                _realtimeLoggerService.Unsubscribe(channelKey);
            }
        }
    }
}
