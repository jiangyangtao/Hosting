namespace Yangtao.Hosting.NLog
{
    public class NLogConfigurationOptions
    {
        internal NLogConfigurationOptions()
        {

        }

        public FileLayoutType FileLayoutType { set; get; } = FileLayoutType.Custom;
    }
}
