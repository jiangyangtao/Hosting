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
            var entityTypes = ModelTypeBuilder.GetEntityModleTypes();
            services.RegisterRepository(entityTypes, nameof(RegisterEntityRepository));

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

        private static void RegisterEntityRepository<TEntity>(IServiceCollection services) where TEntity : BaseEntity<string>, new()
        {
            services.AddScoped<IEntityRepository<TEntity>, EntityRepository<TEntity>>();
        }

        private static void RegisterEntityRepository<TEntity, TKeyType>(IServiceCollection services) where TEntity : BaseEntity<TKeyType>, new() where TKeyType : struct
        {
            services.AddScoped<IEntityRepository<TEntity, TKeyType>, EntityRepository<TEntity, TKeyType>>();
        }

        private static void RegisterViewRepository<TView>(IServiceCollection services) where TView : BaseView
        {
            services.AddScoped<IViewRepositoryProvider<TView>, ViewRepository<TView>>();
        }
    }
}
