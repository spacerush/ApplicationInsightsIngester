using Collector.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Models.ViewModels.Requests
{
    public class RequestsWeeklyViewModel
    {
        public List<RequestPayload> RequestPayloads { get; set; }

        public RequestsWeeklyViewModel(ITelemetryRetrievalService telemetryRetrievalService)
        {
            this.RequestPayloads = telemetryRetrievalService.GetRequestPayloads(DateTime.UtcNow.AddDays(-7), DateTime.UtcNow);
        }
    }
}
