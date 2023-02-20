using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.Mvc.HttpResponseResult.HttpResult;

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
                options.InvalidModelStateResponseFactory = ModelValidationFailHandle;
            });

            return services;
        }

        /// <summary>
        /// 添加模型验证
        /// </summary>
        /// <param name="mvcBuilder"></param>
        /// <returns></returns>
        public static IMvcBuilder AddModelValidation(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = ModelValidationFailHandle;
            });

            return mvcBuilder;
        }

        private static IActionResult ModelValidationFailHandle(ActionContext context)
        {
            var errorMessage = context.ModelState.GetValidationSummary();
            return new HttpBadRequestResult(errorMessage);
        }
    }
}