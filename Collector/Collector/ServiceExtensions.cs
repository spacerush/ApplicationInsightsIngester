using Collector.Repositories;
using Collector.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepoWrapper, RepoWrapper>();
        }

        public static void AddCustomTelemetryService(this IServiceCollection services)
        {
            services.AddScoped<ICustomTelemetryService, CustomTelemetryService>();
        }
    }

}
