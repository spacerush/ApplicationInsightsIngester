using Collector.Models;
using Collector.Models.Documents;
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

        List<TelemetryKey> GetAllTelemetryKeys();

        void AddTelemetryKey(string applicationId, string username);

        bool CheckTelemetryKey(string applicationId, string keyData);

        void ExpireTelemetryKey(Guid keyId);

        void LogRejectedTelemetry(string applicationId, string keyData, string telemetryData, string reason);

        List<RejectedTelemetry> GetRejectedTelemetry(int hours);

    }
}
