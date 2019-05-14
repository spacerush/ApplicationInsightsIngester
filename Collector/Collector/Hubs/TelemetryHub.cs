using Collector.Hubs;
using Collector.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Hubs
{
    public class TelemetryHub : Hub<IJavascriptClient>
    {
        private readonly ITelemetryRetrievalService _retrievalService;

        public TelemetryHub(ITelemetryRetrievalService telemetryRetrievalService)
        {
            _retrievalService = telemetryRetrievalService;
        }      

    }

}
