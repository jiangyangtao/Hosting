﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Yangtao.Hosting.Endpoint
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiVersion(this IServiceCollection services)
        {
            var defaultApiVersion = new ApiVersion(1, 0);
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.UseApiBehavior = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = defaultApiVersion;
            });
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
                options.DefaultApiVersion = defaultApiVersion;
            });

            return services;
        }
    }
}
