namespace Yangtao.Hosting.NLog.NoticeLog
{
    public abstract class NoticeOptionsBase : LogOptionsBase
    {
        /// <summary>
        /// 并行处理数
        /// </summary>
        public int Concurrency { set; get; } = 1;

        /// <summary>
        /// 队列容量
        /// </summary>
        public int QueueCapacity { set; get; } = 5000;

        /// <summary>
        /// 异步处理后等待时间，单位：毫秒
        /// 默认 1000
        /// </summary>
        public int AfterAsyncDelay { set; get; } = 1000;
    }
}
