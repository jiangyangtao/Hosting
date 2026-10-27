using MimeKit;

namespace Yangtao.Hosting.Email.Abstractions
{
    /// <summary>
    /// 邮件服务
    /// </summary>
    public interface IEmailService
    {
        Task<string> SendAsync(MimeMessage message, CancellationToken cancellationToken = default);

        Task SendAsync(IEnumerable<MimeMessage> messages, Action<MimeMessage, string>? sendProgress, CancellationToken cancellationToken = default);
    }
}
