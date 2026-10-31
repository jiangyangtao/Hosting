namespace Yangtao.Hosting.NLog.NoticeLog
{
    public abstract class NoticeOptionsBase : LogOptionsBase
    {
        public int Concurrency { set; get; }

        public int QueueCapacity { set; get; }
    }
}
