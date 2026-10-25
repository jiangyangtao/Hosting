using Microsoft.Extensions.Options;
using MimeKit;
using System.Runtime.CompilerServices;
using Yangtao.Hosting.Email.Abstractions;

namespace Yangtao.Hosting.Email.Services
{
    internal class EmailService : EmailServiceBase, IEmailService
    {
        public EmailService(IOptions<EmailOptions> options) : base(options.Value)
        {
        }

        public async Task<string> SendAsync(MimeMessage message, CancellationToken cancellationToken = default)
        {
            message.From.Add(SenderAddress);

            using var client = await GetClientAsync(cancellationToken);

            // 发送邮件
            var result = await client.SendAsync(message, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);

            return result;
        }

        public async Task SendAsync(IEnumerable<MimeMessage> messages, Action<MimeMessage, string>? sendProgress, CancellationToken cancellationToken = default)
        {
            await foreach (var (message, result) in SendAllAsync(messages, cancellationToken))
            {
                sendProgress?.Invoke(message, result);
            }
        }

        private async IAsyncEnumerable<(MimeMessage, string)> SendAllAsync(IEnumerable<MimeMessage> messages, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            using var client = await GetClientAsync(cancellationToken);
            foreach (var message in messages)
            {
                message.From.Add(SenderAddress);

                var result = await client.SendAsync(message, cancellationToken);
                yield return (message, result);
            }

            await client.DisconnectAsync(true, cancellationToken);
        }
    }
}
