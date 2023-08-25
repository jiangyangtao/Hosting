using Castle.DynamicProxy;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.Repository.Abstractions;

namespace Yangtao.Hosting.Repository.Core
{
    public class EntityTypeBuilder
    {
        public EntityTypeBuilder()
        {
        }

        public static Type[] GetEntityTypes()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var modelType = typeof(BaseEntity);
            var entityTypes = assemblies.SelectMany(assemblie => assemblie.GetTypes().Where(t => t.BaseType != null && t.BaseType == modelType)).ToArray();
            return entityTypes.Where(a => a.HasInterface<IProxyTargetAccessor>() == false).ToArray();  // 去除懒加载创建的实体代理
        }
    }
}
