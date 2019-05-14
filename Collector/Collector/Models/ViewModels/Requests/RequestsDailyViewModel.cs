using Collector.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Models.ViewModels.Requests
{
    public class RequestsDailyViewModel
    {
        public List<RequestPayload> RequestPayloads { get; set; }

        public RequestsDailyViewModel(ITelemetryRetrievalService telemetryRetrievalService)
        {
            this.RequestPayloads = telemetryRetrievalService.GetRequestPayloads(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow);
        }
    }
}
