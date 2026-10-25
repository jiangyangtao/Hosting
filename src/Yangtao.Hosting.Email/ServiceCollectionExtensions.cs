using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.Email.Abstractions;
using Yangtao.Hosting.Email.Services;

namespace Yangtao.Hosting.Email
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEmail(this IServiceCollection services, Action<EmailOptions> optionAction)
        {
            var emailOptions = new EmailOptions();
            optionAction(emailOptions);

            emailOptions.Validate();

            services.Configure<EmailOptions>(options =>
            {
                options.EmailName = emailOptions.EmailName;
                options.Server = emailOptions.Server;
                options.Port = emailOptions.Port;
                options.Account = emailOptions.Account;
                options.Password = emailOptions.Password;
            });
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<IAsyncEmailService, AsyncEmailService>();
            return services;
        }
    }
}
