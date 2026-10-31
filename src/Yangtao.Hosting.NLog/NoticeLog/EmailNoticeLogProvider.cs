using NLog;
using NLog.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yangtao.Hosting.NLog.Abstractions;

namespace Yangtao.Hosting.NLog.NoticeLog
{
    internal class EmailNoticeLogProvider : INoticeLogProvider
    {
        public Layout Layout => throw new NotImplementedException();

        public Task WriteAsync(string formatLog, LogEventInfo logEvent, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
