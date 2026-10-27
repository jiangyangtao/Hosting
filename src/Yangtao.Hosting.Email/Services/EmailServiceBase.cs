using MailKit.Net.Smtp;
using MimeKit;

namespace Yangtao.Hosting.Email.Services
{
    internal class EmailServiceBase
    {
        protected readonly EmailOptions _emailOptions;
        protected readonly MailboxAddress SenderAddress;

        public EmailServiceBase(EmailOptions emailOptions)
        {
            _emailOptions = emailOptions;

            SenderAddress = new MailboxAddress(_emailOptions.EmailName, _emailOptions.Account);
        }


        protected async Task<SmtpClient> GetClientAsync(CancellationToken cancellationToken = default)
        {
            var client = new SmtpClient();
            await client.ConnectAsync(_emailOptions.Server, _emailOptions.Port, true, cancellationToken);
            await client.AuthenticateAsync(_emailOptions.Account, _emailOptions.Password, cancellationToken);

            return client;
        }

        protected static async Task<SmtpClient> GetClientAsync(EmailOptions emailOptions, CancellationToken cancellationToken = default)
        {
            var client = new SmtpClient();
            await client.ConnectAsync(emailOptions.Server, emailOptions.Port, true, cancellationToken);
            await client.AuthenticateAsync(emailOptions.Account, emailOptions.Password, cancellationToken);

            return client;
        }
    }
}
