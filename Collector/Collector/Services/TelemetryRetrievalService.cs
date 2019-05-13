using Collector.Models;
using Collector.Models.Documents;
using Collector.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Services
{
    public class TelemetryRetrievalService : ITelemetryRetrievalService
    {

        private readonly IRepositoryWrapper repositoryWrapper;

        public TelemetryRetrievalService(IRepositoryWrapper repositoryWrapper)
        {
            this.repositoryWrapper = repositoryWrapper;
        }

        public long CountTelemetry()
        {
            return this.repositoryWrapper.TelemetryRepository.Count<TelemetryContainer>(w => w.Id != null);
        }

        public List<AggregateDependencyDuration> GetLatestDependencyDurations()
        {
            // TODO: use better method than getting last hour worth of stuff to throw it all away except for the most recent thing!
            var results = GetDependencyDurations(1);
            results = results.OrderByDescending(o => o.TimeStamp).Take(1).ToList();
            return results;
        }

        public List<string> GetRawEvents(int hours)
        {
            List<string> results = new List<string>();
            var matches = this.repositoryWrapper.TelemetryRepository.GetAll<TelemetryContainer>(f => f.AddedAtUtc >= DateTime.UtcNow.AddHours(-1 * hours)).ToList();
            foreach (var item in matches.OrderByDescending(o => o.UtcDate))
            {
                results.Add(item.TelemetryData);
            }
            return results;
        }

        public List<TelemetryMetadata> GetMetadata(int hours)
        {
            List<TelemetryMetadata> results = new List<TelemetryMetadata>();
            var matches = this.repositoryWrapper.TelemetryRepository.GetAll<TelemetryContainer>(f => f.AddedAtUtc >= DateTime.UtcNow.AddHours(-1 * hours)).ToList();
            foreach (var item in matches.OrderByDescending(o => o.UtcDate))
            {
                var result = new TelemetryMetadata();
                result.ApplicationId = item.ApplicationId;
                result.DateTimeOffset = item.UtcDate;
                result.TelemetryLength = item.TelemetryData.Length;
                results.Add(result);
            }
            return results;
        }

        public List<TelemetryKey> GetAllTelemetryKeys()
        {
            var result = new List<TelemetryKey>();
            var matches = this.repositoryWrapper.TelemetryKeyRepository.GetAll<TelemetryKey>(f => f.Id != null).ToList();
            result = matches;
            return result;
        }

        public List<AggregateDependencyDuration> GetDependencyDurations(int hours)
        {
            List<AggregateDependencyDuration> results = new List<AggregateDependencyDuration>();

            var matches = this.repositoryWrapper.TelemetryRepository.GetAll<TelemetryContainer>(f => f.AddedAtUtc >= DateTime.UtcNow.AddHours(-1 * hours)).ToList();
            foreach (var item in matches)
            {
                // try to protect against Newtonsoft.Json.JsonReaderException jsonReaderException by
                // checking that the telemetry data string contains the two things we are interested
                // in before parsing it into an object.
                if (item.TelemetryData.Contains("metric") && item.TelemetryData.Contains("Dependency duration"))
                {
                    dynamic telemetryItems = JArray.Parse(item.TelemetryData);
                    foreach (dynamic telem in telemetryItems)
                    {
                        if (telem.Payload.TelemetryType == "metric" && telem.Payload.Name == "Dependency duration")
                        {
                            AggregateDependencyDuration aggregate = new AggregateDependencyDuration();
                            aggregate.ApplicationId = item.ApplicationId;
                            aggregate.TimeStamp = telem.Timestamp;
                            aggregate.Sum = telem.Payload.Sum;
                            aggregate.Min = telem.Payload.Min;
                            aggregate.Max = telem.Payload.Max;
                            aggregate.StandardDeviation = telem.Payload.StandardDeviation;
                            var payload = telem.Payload as JToken;
                            foreach (JProperty prop in payload)
                            {
                                if (prop.Name == "Dependency.Type")
                                {
                                    aggregate.DependencyType = (string)prop.Value;
                                }
                            }
                            aggregate.Count = telem.Payload.Count;
                            results.Add(aggregate);
                        }
                    }
                }
            }
            return results;
        }

        public List<RequestPayload> GetRequestPayloads(DateTime startDateTime, DateTime endDateTime)
        {
            List<RequestPayload> results = new List<RequestPayload>();

            var matches = this.repositoryWrapper.TelemetryRepository.GetAll<TelemetryContainer>(f => f.AddedAtUtc >= startDateTime && f.AddedAtUtc <= endDateTime).ToList();
            foreach (var item in matches)
            {
                if (item.TelemetryData.Contains("Payload") && item.TelemetryData.Contains("TelemetryType") && item.TelemetryData.Contains("request"))
                {
                    var telemetryItems = JArray.Parse(item.TelemetryData);

                    foreach (dynamic telem in telemetryItems)
                    {
                        RequestPayload requestPayload = new RequestPayload();
                        requestPayload.Metadata = new RequestPayloadMetadata();
                        requestPayload.Metadata.Level = telem.Level;
                        requestPayload.Metadata.Timestamp = telem.Timestamp;
                        requestPayload.TelemetryType = telem.Payload.TelemetryType;
                        requestPayload.Name = telem.Payload.Name;
                        requestPayload.Duration = telem.Payload.Duration;
                        requestPayload.Id = telem.Payload.Id;
                        requestPayload.ResponseCode = telem.Payload.ResponseCode;
                        requestPayload.Success = telem.Payload.Success;
                        requestPayload.Url = telem.Payload.Url;
                        requestPayload.DeveloperMode = telem.Payload.DeveloperMode;
                        requestPayload.ai_component_version = telem.Payload.ai_component_version;
                        requestPayload.ai_cloud_role_instance = telem.Payload.ai_cloud_role_instance;
                        requestPayload.ai_operation_id = telem.Payload.ai_operation_id;
                        requestPayload.ai_operation_name = telem.Payload.ai_operation_name;
                        requestPayload.ai_location_ip = telem.Payload.ai_location_ip;
                        requestPayload.ai_DeveloperMode = telem.Payload.ai_DeveloperMode;
                        requestPayload.ServerName = telem.Payload.ServerName;
                        results.Add(requestPayload);
                    }
                }
            }
            return results;
        }


        public List<RejectedTelemetry> GetRejectedTelemetry(int hours)
        {
            var matches = this.repositoryWrapper.RejectedTelemetryRepository.GetAll<RejectedTelemetry>(f => f.AddedAtUtc >= DateTime.UtcNow.AddHours(-1 * hours)).ToList();
            return matches;
        }
    }
}
