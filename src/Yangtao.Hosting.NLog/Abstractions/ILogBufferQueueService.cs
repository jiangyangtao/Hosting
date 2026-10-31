namespace Yangtao.Hosting.NLog.Abstractions
{
    public interface ILogBufferQueueService
    {
        bool TryPublish(LogInfo logInfo);

        IRealtimeLogSubscription Subscribe(CancellationToken cancellationToken);

        internal void Unsubscribe(Guid clientId);
    }
}
