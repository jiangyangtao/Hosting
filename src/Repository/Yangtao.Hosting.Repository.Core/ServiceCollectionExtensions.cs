using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Yangtao.Hosting.Repository.Abstractions;

namespace Yangtao.Hosting.Repository.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, DbContext dbContext)
        {
            services.AddHttpContextAccessor();
            services.Configure<RepositoryOptions>(a => a.DbContext = dbContext);

            var registerMethod = typeof(ServiceCollectionExtensions).GetMethod(nameof(RegisterRepository), BindingFlags.Static | BindingFlags.NonPublic);
            var entityTypes = EntityTypeBuilder.GetEntityTypes();
            foreach (var type in entityTypes)
            {
                var method = registerMethod.MakeGenericMethod(type);
                var r = method.GetGenericMethodDefinition();
                method.Invoke(null, new object[] { services });
            }

            return services;
        }

        private static void RegisterRepository<TEntity>(IServiceCollection services) where TEntity : BaseEntity
        {
            services.AddScoped<IRepository<TEntity>, Repository<TEntity>>();
        }
    }
}
