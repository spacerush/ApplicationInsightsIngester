using Collector.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Models.ViewModels.Raw
{
    public class LastHourRawViewModel
    {

        private readonly List<string> rawTelemetry;
        public List<string> RawTelemetry { get { return this.rawTelemetry; } }

        public LastHourRawViewModel(ICustomTelemetryService customTelemetryService)
        {
            this.rawTelemetry = customTelemetryService.GetRawEvents(1);
        }
    }
}
