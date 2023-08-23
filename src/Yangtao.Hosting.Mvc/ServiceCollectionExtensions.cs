using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

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
    }
}