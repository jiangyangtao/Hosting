namespace Yangtao.Hosting.Logger.Options
{
    public abstract class NoticeLoggerOptionsBase : LoggerOptionsBase
    {
        public string TitleFormat { set; get; } = " {Category} - {Level}";
    }
}
