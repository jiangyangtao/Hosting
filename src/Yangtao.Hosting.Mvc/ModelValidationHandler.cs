using Microsoft.AspNetCore.Mvc;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.Mvc.HttpResponseResult.HttpResult;

namespace Yangtao.Hosting.Mvc
{
    internal static class ModelValidationHandler
    {
        public static IActionResult FailHandle(ActionContext context)
        {
            var errorMessage = context.ModelState.GetValidationSummary();
            return new HttpBadRequestResult(errorMessage);
        }
    }
}
