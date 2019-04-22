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
    public class CustomTelemetryService : ICustomTelemetryService
    {
        private readonly IRepositoryWrapper repositoryWrapper;

        public CustomTelemetryService(IRepositoryWrapper repositoryWrapper)
        {
            this.repositoryWrapper = repositoryWrapper;
        }

        public void RecordTelemetry(string telemetry, string applicationId)
        {
            TelemetryContainer telemetryContainer = new TelemetryContainer();
            telemetryContainer.TelemetryData = telemetry;
            telemetryContainer.UtcDate = DateTime.UtcNow;
            telemetryContainer.ApplicationId = applicationId;
            this.repositoryWrapper.TelemetryRepository.AddOne<TelemetryContainer>(telemetryContainer);
        }

        public long CountTelemetry()
        {
            return this.repositoryWrapper.TelemetryRepository.Count<TelemetryContainer>(w => w.Id != null);
        }


        public List<AggregateDependencyDuration> GetDependencyDurations(int hours)
        {
            List<AggregateDependencyDuration> results = new List<AggregateDependencyDuration>();

            var matches = this.repositoryWrapper.TelemetryRepository.GetAll<TelemetryContainer>(f => f.AddedAtUtc >= DateTime.UtcNow.AddHours(-1*hours)).ToList();
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
    }
}
