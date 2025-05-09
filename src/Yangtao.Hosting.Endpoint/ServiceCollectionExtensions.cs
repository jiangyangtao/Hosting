﻿using Asp.Versioning;
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
                  options.ReportApiVersions = false;
                  options.AssumeDefaultVersionWhenUnspecified = true;
                  options.DefaultApiVersion = defaultApiVersion;
              }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
                options.DefaultApiVersion = defaultApiVersion;
            }).AddMvc();

            return services;
        }
    }
}
