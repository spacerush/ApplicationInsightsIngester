using Collector.Dto;
using Collector.Hubs;
using Collector.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Collector.Workers
{
    public class CurrentTimeWorker : BackgroundService
    {
        public IServiceProvider Services { get; }

        public CurrentTimeWorker(IServiceProvider services)
        {
            Services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = Services.CreateScope())
                {
                    var scopedRetrievalService = scope.ServiceProvider.GetRequiredService<ITelemetryRetrievalService>();
                    IHubContext<TelemetryHub, IJavascriptClient> scopedHubContext = scope.ServiceProvider.GetRequiredService<IHubContext<TelemetryHub, IJavascriptClient>>();
                    await scopedHubContext.Clients.All.ReceiveMessage(new MessageEnvelope("Background service CurrentTimeWorker is working."));
                }
                await Task.Delay(60000);
            }
        }
    }
}
