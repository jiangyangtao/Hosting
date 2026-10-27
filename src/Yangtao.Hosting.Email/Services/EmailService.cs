using Microsoft.Extensions.Options;
using MimeKit;
using System.Runtime.CompilerServices;
using Yangtao.Hosting.Email.Abstractions;
using Yangtao.Hosting.Extensions;

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

        public async Task<string> SendAsync(EmailOptions options, MimeMessage message, CancellationToken cancellationToken = default)
        {
            options.Validate();
            var senderAddress = new MailboxAddress(_emailOptions.EmailName, _emailOptions.Account);
            message.From.Add(senderAddress);

            using var client = await GetClientAsync(options, cancellationToken);

            // 发送邮件
            var result = await client.SendAsync(message, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);

            return result;
        }


        public async Task SendAsync(IEnumerable<MimeMessage> messages, Action<MimeMessage, string>? sendProgress, CancellationToken cancellationToken = default)
        {
            if (messages.IsNullOrEmpty()) return;

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



        public async Task SendAsync(EmailOptions options, IEnumerable<MimeMessage> messages, Action<MimeMessage, string>? sendProgress, CancellationToken cancellationToken = default)
        {
            options.Validate();
            if (messages.IsNullOrEmpty()) return;

            await foreach (var (message, result) in SendAllAsync(options, messages, cancellationToken))
            {
                sendProgress?.Invoke(message, result);
            }
        }

        private async IAsyncEnumerable<(MimeMessage, string)> SendAllAsync(EmailOptions options, IEnumerable<MimeMessage> messages, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            using var client = await GetClientAsync(options, cancellationToken);
            var senderAddress = new MailboxAddress(_emailOptions.EmailName, _emailOptions.Account);
            foreach (var message in messages)
            {
                message.From.Add(senderAddress);

                var result = await client.SendAsync(message, cancellationToken);
                yield return (message, result);
            }

            await client.DisconnectAsync(true, cancellationToken);
        }
    }
}
