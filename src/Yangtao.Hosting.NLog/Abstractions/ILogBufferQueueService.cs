namespace Yangtao.Hosting.NLog.Abstractions
{
    public interface ILogBufferQueueService 
    {
        bool TryPublish(LogEntry entry);

        ILogSubscription Subscribe(CancellationToken cancellationToken);

        internal void Unsubscribe(Guid clientId);
    }
}
