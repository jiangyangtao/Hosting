namespace Yangtao.Hosting.FrontendApi.Attributes
{
    /// <summary>
    /// 模块
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ModuleAttribute : Attribute
    {
        public ModuleAttribute(Type module)
        {
            ModuleType = module;
        }

        public Type ModuleType { set; get; }
    }
}
