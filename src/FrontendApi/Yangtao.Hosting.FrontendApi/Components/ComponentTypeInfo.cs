namespace Yangtao.Hosting.FrontendApi.Components
{
    internal class ComponentTypeInfo<TAttribute> where TAttribute : Attribute
    {
        public ComponentTypeInfo(Type componentType, TAttribute componentAttribute)
        {
            ComponentType = componentType;
            ComponentAttribute = componentAttribute;
        }

        public Type ComponentType { set; get; }

        public TAttribute ComponentAttribute { set; get; }
    }
}
