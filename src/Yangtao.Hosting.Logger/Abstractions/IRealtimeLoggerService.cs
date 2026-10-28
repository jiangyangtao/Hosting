using System.Threading.Channels;

namespace Yangtao.Hosting.Logger.Abstractions
{
    public interface IRealtimeLoggerService
    {
        internal void Publish(LogInfo logInfo, Exception? exception);

        ChannelReader<ILogInfo> Subscribe(string channelKey);

        void Unsubscribe(string channelKey);
    }
}
