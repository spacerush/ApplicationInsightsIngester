using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Collector.Hubs;
using Collector.Services;
using Collector.Workers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Collector
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCookieManager(options =>
            {
                options.AllowEncryption = false;
                options.ThrowForPartialCookies = true;
                options.ChunkSize = null;
                options.DefaultExpireTimeInDays = 7;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var client = new MongoClient("mongodb://localhost:27017/AppTelemetry");
            services.AddSingleton<IMongoClient>(c => client);
            services.ConfigureRepositoryWrapper();
            services.AddCustomTelemetryService();
            services.AddTelemetryRetrievalService();

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSignalR();

            services.AddHostedService<CurrentTimeWorker>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // see https://www.billbogaiv.com/posts/net-core-hosted-on-subdirectories-in-nginx for more information about this .UsePathBase
            // to my knowledge the leading forward slash is important! see appsettings.json.
            app.UsePathBase(Configuration["pathBase"]);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSignalR(router =>
            {
                router.MapHub<TelemetryHub>(Configuration["pathBase"] + "/TelemetryHub");
            });

        }
    }
}
