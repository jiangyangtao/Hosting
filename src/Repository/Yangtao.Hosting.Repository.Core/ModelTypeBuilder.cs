using Castle.DynamicProxy;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.Repository.Abstractions;

namespace Yangtao.Hosting.Repository.Core
{
    public class ModelTypeBuilder
    {
        public ModelTypeBuilder()
        {
        }

        public static Type[] GetEntityModleTypes() => GetModleTypes(typeof(BaseEntity));

        public static Type[] GetViewModleTypes() => GetModleTypes(typeof(BaseView));

        private static Type[] GetModleTypes(Type modelType)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var entityTypes = assemblies.SelectMany(assemblie => assemblie.GetTypes().Where(t => t.BaseType != null && t.BaseType == modelType)).ToArray();
            return entityTypes.Where(a => a.HasInterface<IProxyTargetAccessor>() == false).ToArray();  // 去除懒加载创建的实体代理
        }
    }
}
