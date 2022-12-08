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
                // 模型校验失败处理
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errorMessage = context.ModelState.GetValidationSummary();
                    return new JsonResult(new MessageModel<string>()
                    {
                        Success = false,
                        Status = 400,
                        Msg = errorMessage,
                        Data = null,
                    });
                };
            });

            return services;
        }

        public static IMvcBuilder AddModelValidation(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.ConfigureApiBehaviorOptions(options =>
            {
                // 模型校验失败处理
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errorMessage = context.ModelState.GetValidationSummary();
                    var resultContent = new { Code = 400, Message = errorMessage, };
                    var result = new JsonResult(resultContent)
                    {
                        StatusCode = 400,
                    };
                    return result;
                };
            });

            return mvcBuilder;
        }
    }
}