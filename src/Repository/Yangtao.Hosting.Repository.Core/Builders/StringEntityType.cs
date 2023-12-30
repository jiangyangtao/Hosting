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

        public abstract void Register(IServiceCollection services);

        protected void RegisterRepository(MethodInfo methodInfo, IServiceCollection services)
        {
            if (Types.Count == 0) return;

            foreach (var type in Types)
            {
                var method = RegisterRepositoryMethod.MakeGenericMethod(type);
                method.GetGenericMethodDefinition();
                method.Invoke(null, new object[] { services });
            }
        }

        protected abstract MethodInfo RegisterRepositoryMethod { get; }

        protected static readonly Type IgnoreGenericType = typeof(BaseEntity<TKeyType>);

        public abstract Type IgnoreType { get; }

    }

    internal class StringEntityType : EntityTypeBase<string>
    {
        protected override MethodInfo RegisterRepositoryMethod => typeof(StringEntityType).GetMethod(nameof(AddService));

        public override Type IgnoreType => typeof(BaseEntity);

        public static void AddService<TEntity>(IServiceCollection services) where TEntity : class, IEntity<string>, new()
        {
            services.AddScoped<IEntityRepository<TEntity>, EntityRepository<TEntity>>();
        }

        public override void Register(IServiceCollection services) => RegisterRepository(RegisterRepositoryMethod, services);
    }

    internal class GuidEntityType : EntityTypeBase<Guid>
    {
        protected override MethodInfo RegisterRepositoryMethod => typeof(GuidEntityType).GetMethod(nameof(AddService));

        public override Type IgnoreType => typeof(GuidBaseEntity);

        public static void AddService<TEntity>(IServiceCollection services) where TEntity : class, IEntity<Guid>, new()
        {
            services.AddScoped<IGuidEntityRepository<TEntity>, GuidEntityRepository<TEntity>>();
        }

        public override void Register(IServiceCollection services) => RegisterRepository(RegisterRepositoryMethod, services);
    }

    internal class IntegerEntityType : EntityTypeBase<int>
    {
        protected override MethodInfo RegisterRepositoryMethod => typeof(IntegerEntityType).GetMethod(nameof(AddService));

        public override Type IgnoreType => typeof(IntegerBaseEntity);

        public static void AddService<TEntity>(IServiceCollection services) where TEntity : class, IEntity<int>, new()
        {
            services.AddScoped<IIntegerEntityRepository<TEntity>, IntegerEntityRepository<TEntity>>();
        }

        public override void Register(IServiceCollection services) => RegisterRepository(RegisterRepositoryMethod, services);
    }

    internal class BigIntegerEntityType : EntityTypeBase<long>
    {
        protected override MethodInfo RegisterRepositoryMethod => typeof(BigIntegerEntityType).GetMethod(nameof(AddService));

        public override Type IgnoreType => typeof(BigIntegerBaseEntity);

        public static void AddService<TEntity>(IServiceCollection services) where TEntity : class, IEntity<long>, new()
        {
            services.AddScoped<IBigIntegerEntityRepository<TEntity>, BigIntegerEntityRepository<TEntity>>();
        }

        public override void Register(IServiceCollection services) => RegisterRepository(RegisterRepositoryMethod, services);
    }
}
