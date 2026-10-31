using Microsoft.Extensions.Logging;
using NLog;
using NLog.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yangtao.Hosting.Email.Abstractions;
using Yangtao.Hosting.Email;
using Yangtao.Hosting.MessageQueue.BufferQueue.Abstracts;
using Yangtao.Hosting.NLog.Abstractions;
using Microsoft.Extensions.Options;
using System.Net.Http;
using MimeKit.Text;
using MimeKit;

namespace Yangtao.Hosting.NLog.NoticeLog
{
    internal class EmailNoticeLogProvider : INoticeLogProvider
    {
        private readonly string _serviceName;

        private readonly IEmailService _emailService;
        private readonly EmailNoticeOptions _emailNoticeOptions;
        private readonly IBufferQueue<LogInfo> _emailLogQueue;
        private readonly EmailOptions _emailOptions;

        public EmailNoticeLogProvider(IOptions<NLogConfigurationOptions> options, IBufferQueueProvider bufferQueueProvider)
        {
            _serviceName = options.Value.ServiceName;
            _emailNoticeOptions = options.Value.EmailNoticeOptions ?? throw new NullReferenceException(nameof(NLogConfigurationOptions.EmailNoticeOptions));
            _emailOptions = _emailNoticeOptions.EmailOptions;

            _emailLogQueue = bufferQueueProvider.CreateBufferQueue<LogInfo>("EmailLogQueue", async (item) => await SendEmailAsync(item), _emailNoticeOptions.Concurrency, _emailNoticeOptions.QueueCapacity);
        }

        public Layout Layout => _emailNoticeOptions.Layout ?? throw new NullReferenceException(nameof(EmailNoticeOptions.Layout));

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
