using Microsoft.Extensions.DependencyInjection;
using NLog.Config;

namespace Yangtao.Hosting.NLog.NoticeLog
{
    internal class NoticeLogBuilder
    {
        private readonly NLogOptions _nlogOptions;
        private readonly IServiceProvider _serviceProvider;

        public NoticeLogBuilder(NLogOptions nlogOptions, IServiceProvider serviceProvider)
        {
            _nlogOptions = nlogOptions;
            _serviceProvider = serviceProvider;
        }

        public LoggingConfiguration ConfigurationFileLog(LoggingConfiguration configuration)
        {
            if (_nlogOptions.EnableAsyncNoticeLog)
            {
                var asyncNoticeTarget = new AsyncNoticeTarget(_serviceProvider);
                configuration.AddTarget(asyncNoticeTarget);
                configuration.AddRule(new LoggingRule("*", asyncNoticeTarget));
            }

            if (_nlogOptions.EmailNoticeOptions != null)
            {
                var emailNoticeLogProvider = _serviceProvider.GetRequiredService<EmailNoticeLogProvider>();
                var emailNoticeTarget = new EmailNoticeTarget(emailNoticeLogProvider, _nlogOptions.EmailNoticeOptions);
                var rule = _nlogOptions.EmailNoticeOptions.BuildLoggingRule(emailNoticeTarget);

                configuration.AddTarget(emailNoticeTarget);
                configuration.LoggingRules.Add(rule);
            }

            if (_nlogOptions.LarkNoticeOptions != null)
            {
                var larkNoticeLogProvider = _serviceProvider.GetRequiredService<LarkNoticeLogProvider>();
                var larkNoticeTarget = new LarkNoticeTarget(larkNoticeLogProvider, _nlogOptions.LarkNoticeOptions);
                var rule = _nlogOptions.LarkNoticeOptions.BuildLoggingRule(larkNoticeTarget);

                configuration.AddTarget(larkNoticeTarget);
                configuration.LoggingRules.Add(rule);
            }


            return configuration;
        }
    }
}
