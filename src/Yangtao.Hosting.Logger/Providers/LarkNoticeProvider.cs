using Microsoft.Extensions.Options;
using Yangtao.Hosting.Common;
using Yangtao.Hosting.Logger.Abstractions;
using Yangtao.Hosting.Logger.Options;
using Yangtao.Hosting.MessageQueue.BufferQueue.Abstracts;

namespace Yangtao.Hosting.Logger.Providers
{
    internal class LarkNoticeProvider : ILoggerNoticeProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly LarkNoticeOptions _larkNoticeOptions;
        private readonly LoggerFormatter _loggerFormatter;
        private readonly IBufferQueue<LoggerMessage> _loggerMessageQueue;

        public LarkNoticeProvider(IHttpClientFactory httpClientFactory, IOptions<LoggerOptions> loggerOptions, IBufferQueueProvider bufferQueueProvider)
        {
            _httpClientFactory = httpClientFactory;

            var _loggerOptions = loggerOptions.Value ?? throw new NullReferenceException(nameof(LoggerOptions));
            _larkNoticeOptions = _loggerOptions.LarkNoticeOptions ?? throw new NullReferenceException(nameof(LoggerOptions.LarkNoticeOptions));

            _loggerFormatter = new LoggerFormatter(_larkNoticeOptions.Format, _larkNoticeOptions.TitleFormat);
            _loggerMessageQueue = bufferQueueProvider.CreateBufferQueue<LoggerMessage>("LarkLoggerQueue", async (item) => await SendMessageAsync(item), _larkNoticeOptions.BufferCapacity);
        }

        public void Notice(ILogInfo logInfo, Exception? exception)
        {
            if (_larkNoticeOptions.HasLevel(logInfo.Level) == false) return;

            var logText = _loggerFormatter.TextFormat(logInfo, exception);
            var title = _loggerFormatter.TitleFormat(logInfo);
            var message = new LoggerMessage
            {
                Title = title,
                Text = logText,
            };
            _ = _loggerMessageQueue.PutAsync(message, default);
        }

        private async Task SendMessageAsync(LoggerMessage loggerMessage)
        {
            var data = new
            {
                msg_type = "text",
                content = new
                {
                    text = $"{loggerMessage.Title} \n\n{loggerMessage.Text}",
                }
            };
            await _httpClientFactory.CreateClient().PostAsync(_larkNoticeOptions.WebhookUrl, new JsonContent(data));
        }
    }
}
