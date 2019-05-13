using Collector.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Models.ViewModels.Requests
{
    public class RequestsIndexViewModel
    {
        public List<RequestPayload> RequestPayloads { get; set; }

        public RequestsIndexViewModel(ITelemetryRetrievalService telemetryRetrievalService)
        {
            this.RequestPayloads = telemetryRetrievalService.GetRequestPayloads(DateTime.UtcNow.AddHours(-1), DateTime.UtcNow);
        }
    }
}
