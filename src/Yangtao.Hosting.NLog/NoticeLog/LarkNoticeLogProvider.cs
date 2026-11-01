using Microsoft.Extensions.Options;
using NLog;
using Yangtao.Hosting.Common;
using Yangtao.Hosting.MessageQueue.BufferQueue.Abstracts;

namespace Yangtao.Hosting.NLog.NoticeLog
{
    internal class LarkNoticeLogProvider
    {
        private readonly string _serviceName;
        private readonly LarkNoticeOptions _larkNoticeOptions;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IBufferQueue<LogInfo> _larkLogQueue;

        public LarkNoticeLogProvider(IOptions<NLogOptions> options, IHttpClientFactory httpClientFactory, IBufferQueueProvider bufferQueueProvider)
        {
            _serviceName = options.Value.ServiceName;
            _larkNoticeOptions = options.Value.LarkNoticeOptions ?? throw new NullReferenceException(nameof(NLogOptions.LarkNoticeOptions));
            _httpClientFactory = httpClientFactory;

            _larkLogQueue = bufferQueueProvider.CreateBufferQueue<LogInfo>("LarkLogQueue", async (item) => await SendMessageAsync(item), _larkNoticeOptions.Concurrency, _larkNoticeOptions.QueueCapacity);
        }

        public async Task WriteAsync(string formatLog, LogEventInfo logEvent, CancellationToken cancellationToken)
        {
            if (logEvent.Level < _larkNoticeOptions.MinLevel) return;
            if (logEvent.Level > _larkNoticeOptions.MaxLevel) return;

            var logInfo = new LogInfo(
                Timestamp: logEvent.TimeStamp,
                Level: logEvent.Level.Name,
                LoggerName: logEvent.LoggerName ?? string.Empty,
                Message: formatLog,
                Exception: logEvent.Exception?.ToString());
            await _larkLogQueue.PutAsync(logInfo, cancellationToken);
        }

        private async Task SendMessageAsync(LogInfo logInfo)
        {
            var title = $"{_serviceName} {logInfo.Level}";
            var data = new
            {
                msg_type = "text",
                content = new
                {
                    text = $"{title} \n\n {logInfo.Message}",
                }
            };
            await _httpClientFactory.CreateClient(LarkNoticeOptions.LarkHttpClient).PostAsync(_larkNoticeOptions.WebhookUrl, new JsonContent(data));
            await Task.Delay(_larkNoticeOptions.AfterAsyncDelay);
        }
    }
}
