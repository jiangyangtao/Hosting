using System.ComponentModel.DataAnnotations;
using System.Threading.Channels;

namespace Yangtao.Hosting.Logger.Options
{
    public class RealtimeLoggerOptions : LoggerOptionsBase
    {
        [Range(1000, 100000)]
        public override int BufferCapacity { set; get; } = 1000;

        public BoundedChannelFullMode ChannelFullMode { set; get; } = BoundedChannelFullMode.DropOldest;


        /// <summary>
        /// 中央日志渠道容量
        /// </summary>
        [Range(10000, 1000000)]
        public int CentralChannelCapacity { set; get; } = 100000;

        /// <summary>
        /// 渠道上限模式
        /// </summary>
        public BoundedChannelFullMode CentralChannelFullMode { set; get; } = BoundedChannelFullMode.DropOldest;

        /// <summary>
        /// 日志输出类型
        /// </summary>
        public LoggerResponseType LoggerResponseType = LoggerResponseType.Text;
    }
}
