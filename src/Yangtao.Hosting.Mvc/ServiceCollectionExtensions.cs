using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.Mvc.Abstractions;

namespace Yangtao.Hosting.Mvc
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加模型验证
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddModelValidation(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ModelValidationHandler.FailHandle;
            });

            return services;
        }

        /// <summary>
        /// 允许所有跨域
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAllowAnyCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.SetIsOriginAllowed(_ => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            });

            return services;
        }

        /// <summary>
        /// 添加应用文档
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationDocument(this IServiceCollection services)
        {
            services.AddSingleton<IApplicationDocumentProvider, ApplicationDocumentProvider>();
            return services;
        }
    }
}