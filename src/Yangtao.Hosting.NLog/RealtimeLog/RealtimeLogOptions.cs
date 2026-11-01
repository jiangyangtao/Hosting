using NLog.Layouts;
using System.ComponentModel.DataAnnotations;
using System.Threading.Channels;

namespace Yangtao.Hosting.NLog.RealtimeLog
{
    public class RealtimeLogOptions : LogOptionsBase
    {
        /// <summary>
        /// 订阅渠道日志容量。
        /// 默认 1000
        /// </summary>
        [Range(1000, 100000)]
        public int SubscribeChannelCapacity { set; get; } = 1000;

        /// <summary>
        /// 订阅渠道上限模式。
        /// 默认丢弃最旧 <see cref="BoundedChannelFullMode.DropOldest"/>
        /// </summary>
        public BoundedChannelFullMode SubscribeChannelFullMode { set; get; } = BoundedChannelFullMode.DropOldest;

        /// <summary>
        /// 主渠道日志容量。
        /// 默认 10000
        /// </summary>
        [Range(10000, 1000000)]
        public int MainChannelCapacity { set; get; } = 10000;

        /// <summary>
        /// 主渠道上限模式。
        /// 默认丢弃最旧 <see cref="BoundedChannelFullMode.DropOldest"/>
        /// </summary>
        public BoundedChannelFullMode MainChannelFullMode { set; get; } = BoundedChannelFullMode.DropOldest;

        /// <summary>
        /// 日志输出类型。
        /// 默认 <see cref="LorResponseType.Json"/>
        /// </summary>
        public LorResponseType LorResponseType = LorResponseType.Json;

        /// <summary>
        /// 心跳间隔时间，单位：毫秒。
        /// 默认 15 秒
        /// </summary>
        public int HeartbeatInterval { set; get; } = 15;

        public override Layout? Layout { set; get; } = RealtimeLogBuilder.DefaultLayout;
    }
}
