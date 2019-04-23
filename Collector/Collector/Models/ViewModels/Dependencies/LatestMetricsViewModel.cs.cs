using Collector.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Models.ViewModels.Dependencies
{
    public class LatestMetricsViewModel
    {

        private readonly List<AggregateDependencyDuration> aggregateDependencyDurations;
        public List<AggregateDependencyDuration> AggregateDependencyDurations { get { return this.aggregateDependencyDurations; } }

        public LatestMetricsViewModel(ICustomTelemetryService customTelemetryService)
        {
            this.aggregateDependencyDurations = customTelemetryService.GetLatestDependencyDurations();
        }
    }
}
