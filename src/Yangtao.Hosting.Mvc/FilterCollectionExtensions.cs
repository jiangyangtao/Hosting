using Microsoft.AspNetCore.Mvc.Filters;
using Yangtao.Hosting.Mvc.Filters;

namespace Yangtao.Hosting.Mvc
{
    public static class FilterCollectionExtensions
    {
        public static FilterCollection AddGlobalExceptionFilter(this FilterCollection filters)
        {
            filters.Add<ExceptionFilter>();
            return filters;
        }
    }
}
