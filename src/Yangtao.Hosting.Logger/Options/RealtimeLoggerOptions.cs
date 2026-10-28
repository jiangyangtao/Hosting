using System.Threading.Channels;

namespace Yangtao.Hosting.Logger.Options
{
    public class RealtimeLoggerOptions : LoggerOptionsBase
    {
        public int ChannelCapacity { set; get; } = 1000;

        public BoundedChannelFullMode ChannelFullMode { set; get; } = BoundedChannelFullMode.DropOldest;
    }
}
