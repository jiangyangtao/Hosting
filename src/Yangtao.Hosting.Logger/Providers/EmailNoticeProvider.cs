using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Yangtao.Hosting.Email;
using Yangtao.Hosting.Email.Abstractions;
using Yangtao.Hosting.Logger.Abstractions;
using Yangtao.Hosting.Logger.Options;
using Yangtao.Hosting.MessageQueue.BufferQueue.Abstracts;

namespace Yangtao.Hosting.Logger.Providers
{
    internal class EmailNoticeProvider : ILoggerNoticeProvider
    {
        private readonly IEmailService _emailService;
        private readonly EmailNoticeOptions _emailNoticeOptions;
        private readonly LoggerFormatter _loggerFormatter;
        private readonly IBufferQueue<LoggerMessage> _loggerMessageQueue;
        private readonly EmailOptions _emailOptions;

        public EmailNoticeProvider(IEmailService emailService, IOptions<LoggerOptions> loggerOptions, IBufferQueueProvider bufferQueueProvider)
        {
            _emailService = emailService;

            var _loggerOptions = loggerOptions.Value ?? throw new NullReferenceException(nameof(LoggerOptions));
            _emailNoticeOptions = _loggerOptions.EmailNoticeOptions ?? throw new NullReferenceException(nameof(LoggerOptions.EmailNoticeOptions));

            _loggerFormatter = new LoggerFormatter(_emailNoticeOptions.Format, _emailNoticeOptions.TitleFormat);
            _emailOptions = _emailNoticeOptions.EmailOptions;

            _loggerMessageQueue = bufferQueueProvider.CreateBufferQueue<LoggerMessage>("EmailLoggerQueue", async (item) => await SendEmailAsync(item), _emailNoticeOptions.BufferCapacity);
        }

        public void Notice(ILogInfo logInfo, Exception? exception)
        {
            if (_emailNoticeOptions.HasLevel(logInfo.Level) == false) return;

            var logText = _loggerFormatter.TextFormat(logInfo, exception);
            var title = _loggerFormatter.TitleFormat(logInfo);
            var message = new LoggerMessage
            {
                Title = title,
                Text = logText,
            };
            _ = _loggerMessageQueue.PutAsync(message, default);
        }

        private async Task SendEmailAsync(LoggerMessage loggerMessage)
        {
            var message = new MimeMessage()
            {
                Subject = loggerMessage.Title,
                Body = new TextPart(TextFormat.Plain) { Text = loggerMessage.Text }
            };
            await _emailService.SendAsync(_emailOptions, message, default);
        }
    }
}
