using System.ComponentModel.DataAnnotations;
using System.Threading.Channels;

namespace Yangtao.Hosting.Logger.Options
{
    /// <summary>
    /// 文件日志配置
    /// </summary>
    public class FileLoggerOptions : LoggerOptionsBase
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        [Required]
        public string? FilePath { set; get; }

        /// <summary>
        /// 文件名
        /// </summary>
        [Required]
        public string? FileName { set; get; }

        [Range(100, 100000)]
        public override int BufferCapacity { set; get; } = 1000;

        public BoundedChannelFullMode ChannelFullMode { set; get; } = BoundedChannelFullMode.DropOldest;

        ///// <summary>
        ///// 队列容量
        ///// </summary>
        //[Range(100, 100000)]
        //public int BufferSize { set; get; } = 5000;

        /// <summary>
        /// 批量落盘 Flush 间隔。
        /// 单位：毫秒
        /// </summary>
        [Range(10, 60000)]
        public int FlushIntervalMillisecond { set; get; } = 50;
    }
}
