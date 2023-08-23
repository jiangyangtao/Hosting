using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Yangtao.Hosting.Mvc
{
    public static class MvcBuilderExtensions
    {
        /// <summary>
        /// 添加模型验证
        /// </summary>
        /// <param name="mvcBuilder"></param>
        /// <returns></returns>
        public static IMvcBuilder AddModelValidation(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = ModelValidationHandler.FailHandle;
            });

            return mvcBuilder;
        }

        public static IMvcBuilder AddDefaultNewtonsoftJson(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Formatting = Formatting.Indented;
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            return mvcBuilder;
        }
    }
}
