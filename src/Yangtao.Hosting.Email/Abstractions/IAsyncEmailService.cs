using MimeKit;

namespace Yangtao.Hosting.Email.Abstractions
{
    /// <summary>
    /// 异步邮件服务
    /// </summary>
    public interface IAsyncEmailService
    {
        Task<bool> SendAsync(MimeMessage message, CancellationToken cancellationToken = default);

        Task<bool> SendAsync(MimeMessage[] messages, CancellationToken cancellationToken = default);
    }
}
