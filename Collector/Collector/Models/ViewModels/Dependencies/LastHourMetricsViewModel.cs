using Collector.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Models.ViewModels.Dependencies
{
    public class LastHourMetricsViewModel
    {

        private readonly List<AggregateDependencyDuration> aggregateDependencyDurations;
        public List<AggregateDependencyDuration> AggregateDependencyDurations { get { return this.aggregateDependencyDurations; } }

        public LastHourMetricsViewModel(ITelemetryRetrievalService telemetryRetrievalService)
        {
            this.aggregateDependencyDurations = telemetryRetrievalService.GetDependencyDurations(1);
        }
    }
}
