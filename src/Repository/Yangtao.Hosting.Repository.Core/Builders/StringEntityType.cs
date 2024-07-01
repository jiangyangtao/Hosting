using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.Repository.Abstractions;
using Yangtao.Hosting.Repository.Core.Providers;

namespace Yangtao.Hosting.Repository.Core.Builders
{
    internal abstract class EntityTypeBase<TKeyType>
    {
        public Type KeyType = typeof(IEntity<TKeyType>);

        protected readonly List<Type> Types;

        public EntityTypeBase()
        {
            Types = new List<Type>();
        }

        public Type[] EntityTypes => Types.ToArray();

        public bool IsGenericType(Type type) => KeyType == type;

        public void Add(Type type)
        {
            if (type.FullName.IsNullOrEmpty()) return;
            if (type.FullName.Contains(IgnoreGenericType.FullName)) return;
            if (type == IgnoreType) return;
            if (type.HasInterface<IProxyTargetAccessor>()) return;

            var hasInterface = type.HasInterface(KeyType);
            if (hasInterface == false) return;

            Types.Add(type);
        }

        public void AddService<TEntity>(IServiceCollection services) where TEntity : class, IEntity<string>, new()
        {
            services.AddScoped<IEntityRepository<TEntity>, EntityRepository<TEntity>>();
        }

        public abstract void Register(IServiceCollection services);

        protected void RegisterRepository(IServiceCollection services)
        {
            if (Types.Count == 0) return;

            foreach (var type in Types)
            {
                var method = RegisterRepositoryMethod.MakeGenericMethod(type);
                method.GetGenericMethodDefinition();
                method.Invoke(null, new object[] { services });
            }
        }

        protected MethodInfo RegisterRepositoryMethod => typeof(EntityTypeBase<TKeyType>).GetMethod(nameof(AddService));

        protected static readonly Type IgnoreGenericType = typeof(BaseEntity<TKeyType>);

        public abstract Type IgnoreType { get; }
    }

    internal class StringEntityType : EntityTypeBase<string>
    {
        public override Type IgnoreType => typeof(BaseEntity);

        public override void Register(IServiceCollection services) => RegisterRepository(services);
    }

    internal class GuidEntityType : EntityTypeBase<Guid>
    {
        public override Type IgnoreType => typeof(GuidBaseEntity);

        public override void Register(IServiceCollection services) => RegisterRepository(services);
    }

    internal class IntegerEntityType : EntityTypeBase<int>
    {
        public override Type IgnoreType => typeof(IntegerBaseEntity);

        public override void Register(IServiceCollection services) => RegisterRepository(services);
    }

    internal class BigIntegerEntityType : EntityTypeBase<long>
    {
        public override Type IgnoreType => typeof(BigIntegerBaseEntity);

        public override void Register(IServiceCollection services) => RegisterRepository(services);
    }
}
