using Collector.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Models.ViewModels
{
    public class IndexViewModel
    {
        public long telemetryCount;
        public long TelemetryCount {
            get
            {
                return this.telemetryCount;
            }
        }
        public IndexViewModel(ITelemetryRetrievalService telemetryRetrievalService)
        {
            this.telemetryCount = telemetryRetrievalService.CountTelemetry();
        }
    }
}
