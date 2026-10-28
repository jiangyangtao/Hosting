namespace Yangtao.Hosting.Logger.Abstractions
{
    public interface ILoggerNoticeProvider
    {
        void Notice(ILogInfo logInfo, Exception? exception);
    }
}
