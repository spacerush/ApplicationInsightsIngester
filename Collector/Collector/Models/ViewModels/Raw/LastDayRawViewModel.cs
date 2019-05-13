using Collector.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Models.ViewModels.Raw
{
    public class LastDayRawViewModel
    {

        private readonly List<string> rawTelemetry;
        public List<string> RawTelemetry { get { return this.rawTelemetry; } }

        public LastDayRawViewModel(ITelemetryRetrievalService telemetryRetrievalService)
        {
            this.rawTelemetry = telemetryRetrievalService.GetRawEvents(24);
        }
    }
}
