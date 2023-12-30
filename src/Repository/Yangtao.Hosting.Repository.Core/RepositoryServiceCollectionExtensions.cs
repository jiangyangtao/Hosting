using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.Repository.Abstractions;
using Yangtao.Hosting.Repository.Core.Builders;
using Yangtao.Hosting.Repository.Core.Providers;

namespace Yangtao.Hosting.Repository.Core
{
    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryCore(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
        {
            services.AddHttpContextAccessor();
            services.AddDbContext<DefaultDbContext>(optionsAction);
            services.RegisterModelRepository();

            services.AddScoped<IDataBaseRepository, DataBaseRepository>();
            return services;
        }

        public static IServiceCollection AddRepositoryCore(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction, Action<DbContextBuilder> builderAction)
        {
            return services.AddRepositoryCore(optionsBuilder =>
            {
                var contextBuilder = new DbContextBuilder();
                builderAction(contextBuilder);
                optionsBuilder.Options.WithExtension(contextBuilder);

                optionsAction(optionsBuilder);
            });
        }

        private static void RegisterModelRepository(this IServiceCollection services)
        {
            var types = ModelTypeBuilder.GetEntityModleTypes();

            //var entityTypes =types.Where(a=>a.Generic)
            services.RegisterRepository(types, nameof(RegisterEntityRepository));

            var viewTypes = ModelTypeBuilder.GetViewModleTypes();
            if (viewTypes.NotNullAndEmpty()) services.RegisterRepository(viewTypes, nameof(RegisterViewRepository));
        }

        private static void RegisterRepository(this IServiceCollection services, Type[] types, string methodName)
        {
            var registerRepositoryMethod = typeof(RepositoryServiceCollectionExtensions).GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic);
            foreach (var type in types)
            {
                var method = registerRepositoryMethod.MakeGenericMethod(type);
                method.GetGenericMethodDefinition();
                method.Invoke(null, new object[] { services });
            }
        }

        private static void RegisterEntityRepository<TEntity>(IServiceCollection services) where TEntity : class, IEntity<string>, new()
        {
            services.AddScoped<IEntityRepository<TEntity>, EntityRepository<TEntity>>();
        }

        private static void RegisterGuidEntityRepository<TEntity>(IServiceCollection services) where TEntity : class, IEntity<Guid>, new()
        {
            services.AddScoped<IGuidEntityRepository<TEntity>, GuidEntityRepository<TEntity>>();
        }

        private static void RegisterIntegerEntityRepository<TEntity>(IServiceCollection services) where TEntity : class, IEntity<int>, new()
        {
            services.AddScoped<IIntegerEntityRepository<TEntity>, IntegerEntityRepository<TEntity>>();
        }

        private static void RegisterBigIntegerEntityRepository<TEntity>(IServiceCollection services) where TEntity : class, IEntity<long>, new()
        {
            services.AddScoped<IBigIntegerEntityRepository<TEntity>, BigIntegerEntityRepository<TEntity>>();
        }

        private static void RegisterViewRepository<TView>(IServiceCollection services) where TView : BaseView
        {
            services.AddScoped<IViewRepositoryProvider<TView>, ViewRepository<TView>>();
        }
    }
}
