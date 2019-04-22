using Collector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Services
{
    public interface ICustomTelemetryService
    {
        void RecordTelemetry(string telemetry, string applicationId);

        List<AggregateDependencyDuration> GetDependencyDurations(int hours);

        List<AggregateDependencyDuration> GetLatestDependencyDurations();

        long CountTelemetry();

        List<string> GetRawEvents(int hours);

        List<TelemetryMetadata> GetMetadata(int hours);

    }
}
