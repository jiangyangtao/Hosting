using Microsoft.Extensions.Options;
using NLog;
using NLog.Layouts;
using Yangtao.Hosting.Common;
using Yangtao.Hosting.MessageQueue.BufferQueue.Abstracts;
using Yangtao.Hosting.NLog.Abstractions;

namespace Yangtao.Hosting.NLog.NoticeLog
{
    internal class LarkNoticeLogProvider : INoticeLogProvider
    {
        private readonly string _serviceName;
        private readonly LarkNoticeOptions _larkNoticeOptions;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IBufferQueue<LogInfo> _larkLogQueue;

        public LarkNoticeLogProvider(IOptions<NLogConfigurationOptions> options, IHttpClientFactory httpClientFactory, IBufferQueueProvider bufferQueueProvider)
        {
            _serviceName = options.Value.ServiceName;
            _larkNoticeOptions = options.Value.LarkNoticeOptions ?? throw new NullReferenceException(nameof(NLogConfigurationOptions.LarkNoticeOptions));
            _httpClientFactory = httpClientFactory;

            _larkLogQueue = bufferQueueProvider.CreateBufferQueue<LogInfo>("LarkLogQueue", async (item) => await SendMessageAsync(item), _larkNoticeOptions.Concurrency, _larkNoticeOptions.QueueCapacity);
        }

        public Layout Layout => _larkNoticeOptions.Layout ?? throw new NullReferenceException(nameof(LarkNoticeOptions.Layout));

        public async Task WriteAsync(string formatLog, LogEventInfo logEvent, CancellationToken cancellationToken)
        {
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
            await _httpClientFactory.CreateClient().PostAsync(_larkNoticeOptions.WebhookUrl, new JsonContent(data));
            await Task.Delay(_larkNoticeOptions.AfterAsyncDelay);
        }
    }
}
