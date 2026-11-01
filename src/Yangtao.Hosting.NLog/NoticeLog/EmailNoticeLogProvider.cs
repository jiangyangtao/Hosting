using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using NLog;
using Yangtao.Hosting.Email;
using Yangtao.Hosting.Email.Abstractions;
using Yangtao.Hosting.MessageQueue.BufferQueue.Abstracts;

namespace Yangtao.Hosting.NLog.NoticeLog
{
    internal class EmailNoticeLogProvider
    {
        private readonly string _serviceName;

        private readonly IEmailService _emailService;
        private readonly EmailNoticeOptions _emailNoticeOptions;
        private readonly IBufferQueue<LogInfo> _emailLogQueue;
        private readonly EmailOptions _emailOptions;

        public EmailNoticeLogProvider(IOptions<NLogOptions> options, IBufferQueueProvider bufferQueueProvider, IEmailService emailService)
        {
            _serviceName = options.Value.ServiceName;
            _emailNoticeOptions = options.Value.EmailNoticeOptions ?? throw new NullReferenceException(nameof(NLogOptions.EmailNoticeOptions));
            _emailOptions = _emailNoticeOptions.EmailOptions;

            _emailLogQueue = bufferQueueProvider.CreateBufferQueue<LogInfo>("EmailLogQueue", async (item) => await SendEmailAsync(item), _emailNoticeOptions.Concurrency, _emailNoticeOptions.QueueCapacity);
            _emailService = emailService;
        }

        public async Task WriteAsync(string formatLog, LogEventInfo logEvent, CancellationToken cancellationToken)
        {
            var logInfo = new LogInfo(
              Timestamp: logEvent.TimeStamp,
              Level: logEvent.Level.Name,
              LoggerName: logEvent.LoggerName ?? string.Empty,
              Message: formatLog,
              Exception: logEvent.Exception?.ToString());
            await _emailLogQueue.PutAsync(logInfo, cancellationToken);
        }

        private async Task SendEmailAsync(LogInfo logInfo)
        {
            var title = $"{_serviceName} {logInfo.Level}";
            if (_emailNoticeOptions.RenderTitle != null) title = _emailNoticeOptions.RenderTitle(logInfo);

            var message = new MimeMessage()
            {
                Subject = title,
                Body = new TextPart(TextFormat.Plain) { Text = logInfo.Message }
            };
            await _emailService.SendAsync(_emailOptions, message, default);
            await Task.Delay(_emailNoticeOptions.AfterAsyncDelay);
        }
    }
}
