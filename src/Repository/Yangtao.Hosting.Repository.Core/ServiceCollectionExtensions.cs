﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.Repository.Abstractions;

namespace Yangtao.Hosting.Repository.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
        {
            services.AddHttpContextAccessor();
            services.AddDbContext<DefaultDbContext>(optionsAction);
            services.RegisterModelRepository();

            return services;
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
            var registerRepositoryMethod = BuildRegisterRepositoryMethod(methodName);
            foreach (var type in types)
            {
                var method = registerRepositoryMethod.MakeGenericMethod(type);
                method.GetGenericMethodDefinition();
                method.Invoke(null, new object[] { services });
            }
        }

        private static void RegisterEntityRepository<TEntity>(IServiceCollection services) where TEntity : BaseEntity
        {
            services.AddScoped<IEntityRepositoryProvider<TEntity>, EntityRepositoryProvider<TEntity>>();
        }

        private static void RegisterViewRepository<TView>(IServiceCollection services) where TView : BaseView
        {
            services.AddScoped<IViewRepositoryProvider<TView>, ViewRepositoryProvider<TView>>();
        }

        private static MethodInfo BuildRegisterRepositoryMethod(string methodName) => typeof(ServiceCollectionExtensions).GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic);
    }
}