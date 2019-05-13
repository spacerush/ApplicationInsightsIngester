using Collector.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Models.ViewModels.Dependencies
{
    public class LastDayMetricsViewModel
    {

        private readonly List<AggregateDependencyDuration> aggregateDependencyDurations;
        public List<AggregateDependencyDuration> AggregateDependencyDurations { get { return this.aggregateDependencyDurations; } }

        public LastDayMetricsViewModel(ITelemetryRetrievalService telemetryRetrievalService)
        {
            this.aggregateDependencyDurations = telemetryRetrievalService.GetDependencyDurations(24);
        }
    }
}
