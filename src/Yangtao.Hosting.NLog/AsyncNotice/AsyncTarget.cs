using NLog;
using NLog.Targets;

namespace Yangtao.Hosting.NLog.AsyncNotice
{
    internal class AsyncTarget : AsyncTaskTarget
    {
        protected override Task WriteAsyncTask(LogEventInfo logEvent, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
