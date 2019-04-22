using Collector.Models.Documents;
using Collector.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Models.ViewModels.Failures
{
    public class LastDayFailuresViewModel
    {

        private readonly List<RejectedTelemetry> rejectedTelemetries;
        public List<RejectedTelemetry> RejectedTelemetries { get { return this.rejectedTelemetries; } }

        public LastDayFailuresViewModel(ICustomTelemetryService customTelemetryService)
        {
            this.rejectedTelemetries = customTelemetryService.GetRejectedTelemetry(24);
        }
    }
}
