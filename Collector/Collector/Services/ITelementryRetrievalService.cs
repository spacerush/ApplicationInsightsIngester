using Collector.Models;
using Collector.Models.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Services
{
    public interface ITelemetryRetrievalService
    {

        long CountTelemetry();

        List<string> GetRawEvents(int hours);

        List<TelemetryMetadata> GetMetadata(int hours);

        List<TelemetryKey> GetAllTelemetryKeys();


        List<AggregateDependencyDuration> GetDependencyDurations(int hours);

        List<AggregateDependencyDuration> GetLatestDependencyDurations();

        List<RequestPayload> GetRequestPayloads(DateTime startDateTime, DateTime endDateTime);

        List<RequestPayload> GetRequestPayloadById(Guid telemetryId);

        List<RejectedTelemetry> GetRejectedTelemetry(int hours);

    }
}
