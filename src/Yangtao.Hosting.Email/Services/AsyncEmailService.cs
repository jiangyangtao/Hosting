using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Yangtao.Hosting.Email.Abstractions;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.MessageQueue.BufferQueue.Abstracts;

namespace Yangtao.Hosting.Email.Services
{
    internal class AsyncEmailService : EmailServiceBase, IAsyncEmailService
    {
        private readonly IBufferQueue<MimeMessage> _messageQueue;
        private readonly IBufferQueue<MimeMessage[]> _messagesQueue;

        private readonly IEmailService _emailService;
        private readonly ILogger<AsyncEmailService> _logger;

        public AsyncEmailService(
            IOptions<EmailOptions> options,
            IBufferQueueProvider bufferQueueProvider,
            IEmailService emailService,
            ILogger<AsyncEmailService> logger) : base(options.Value)
        {
            _messageQueue = bufferQueueProvider.CreateBufferQueue<MimeMessage>("EmailMessageQueue", async (item) => await SendAsync(item), 1000);
            _messagesQueue = bufferQueueProvider.CreateBufferQueue<MimeMessage[]>("EmailMessagesQueue", async (item) => await SendAsync(item), 1, 10000);
            _emailService = emailService;
            _logger = logger;
        }

        public Task<bool> SendAsync(MimeMessage message, CancellationToken cancellationToken = default) => _messageQueue.PutAsync(message, cancellationToken);


        public async Task<bool> SendAsync(MimeMessage[] messages, CancellationToken cancellationToken = default)
        {
            if (messages.IsNullOrEmpty()) return false;

            return await _messagesQueue.PutAsync(messages, cancellationToken);
        }

        private Task SendAsync(MimeMessage message) => _emailService.SendAsync(message, default);

        private async Task SendAsync(MimeMessage[] messages)
        {
            _logger.LogInformation($"Sending email..., count: {messages.Length}");

            await _emailService.SendAsync(messages, (message, result) =>
             {
                 _logger.LogInformation($"Email:[{message.Subject}] is sent, response: {result}");
             }, default);
        }
    }
}
