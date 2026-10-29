using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using Yangtao.Hosting.Logger.Abstractions;
using Yangtao.Hosting.Logger.Options;

namespace Yangtao.Hosting.Logger.Providers
{
    internal class RealtimeLoggerProvider : IRealtimeLoggerProvider
    {
        private readonly IRealtimeLoggerService _realtimeLoggerService;
        private readonly RealtimeLoggerOptions _realtimeLoggerOptions;

        public RealtimeLoggerProvider(IRealtimeLoggerService realtimeLoggerService, IOptions<LoggerOptions> loggerOptions)
        {
            _realtimeLoggerService = realtimeLoggerService;

            var _loggerOptions = loggerOptions.Value ?? throw new NullReferenceException(nameof(LoggerOptions));
            _realtimeLoggerOptions = _loggerOptions.RealtimeLoggerOptions ?? throw new NullReferenceException(nameof(LoggerOptions.RealtimeLoggerOptions));
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
            await context.Response.WriteAsync(ConnectionedResponse, cancellationToken);
            await context.Response.Body.FlushAsync(cancellationToken);

            var channelKey = "RealtimeLogger";
            var reader = _realtimeLoggerService.Subscribe(channelKey);


            try
            {
                //await foreach (var log in reader.ReadAllAsync(cancellationToken))
                //{
                //    var dataResponse = BuildDataResponse(log.FormattedText);
                //    await context.Response.WriteAsync(dataResponse, cancellationToken);
                //    await context.Response.Body.FlushAsync(cancellationToken);
                //}

                while (cancellationToken.IsCancellationRequested == false)
                {
                    var waitTask = reader.WaitToReadAsync(cancellationToken).AsTask();
                    var delayTask = Task.Delay(TimeSpan.FromSeconds(15), cancellationToken);

                    var completed = await Task.WhenAny(waitTask, delayTask);
                    if (completed == delayTask)
                    {
                        // 发送心跳注释行
                        await context.Response.WriteAsync(KeepAliveResponse, cancellationToken);
                        await context.Response.Body.FlushAsync(cancellationToken);
                        continue;
                    }

                    if (!await waitTask) break;

                    while (reader.TryRead(out var log))
                    {
                        var dataResponse = BuildDataResponse(log.FormattedText);
                        await context.Response.WriteAsync(dataResponse, cancellationToken);
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


        private string BuildResponseResult(RealtimeLoggerResponse loggerResponse)
        {
            if (_realtimeLoggerOptions.LoggerResponseType == LoggerResponseType.Json) return loggerResponse.ToJson();

            return loggerResponse.ToString();
        }

        private string ConnectionedResponse => BuildResponseResult(RealtimeLoggerResponse.Connectioned());

        private string KeepAliveResponse => BuildResponseResult(RealtimeLoggerResponse.KeepAlive());

        private string BuildDataResponse(string log) => BuildResponseResult(RealtimeLoggerResponse.Data(log));
    }
}
