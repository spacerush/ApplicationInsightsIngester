using Collector.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Models.ViewModels.Raw
{
    public class DailyChartViewModel
    {
        private readonly List<TelemetryMetadata> metadata;
        public List<TelemetryMetadata> Metadata { get { return metadata; } }

        public string TelemetryLengthLabels
        {
            get
            {
                List<string> result = new List<string>();
                foreach (var item in Metadata.OrderBy(o => o.DateTimeOffset))
                {
                    result.Add(item.DateTimeOffset.UtcDateTime.ToShortDateString() + " " + item.DateTimeOffset.UtcDateTime.ToShortTimeString());
                }
                return JsonConvert.SerializeObject(result);
            }
        }

        public string TelemetryLengthData
        {
            get
            {
                List<int> result = new List<int>();
                foreach (var item in Metadata.OrderBy(o => o.DateTimeOffset))
                {
                    result.Add(item.TelemetryLength);
                }
                return JsonConvert.SerializeObject(result);
            }
        }

        public DailyChartViewModel(ITelemetryRetrievalService telemetryRetrievalService)
        {
            this.metadata = telemetryRetrievalService.GetMetadata(24);

        }
    }
}
