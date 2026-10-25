using MimeKit;

namespace Yangtao.Hosting.Email.Abstractions
{
    public interface IAsyncEmailService
    {
        Task<bool> SendAsync(MimeMessage message, CancellationToken cancellationToken = default);

        Task<bool> SendAsync(MimeMessage[] messages, CancellationToken cancellationToken = default);
    }
}
