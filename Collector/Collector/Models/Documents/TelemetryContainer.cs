using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Models.Documents
{
    public class TelemetryContainer : Document
    {
        public long TelemetryLogId { get; set; }
        public string ApplicationId { get; set; }
        public DateTime UtcDate { get; set; }
        public string TelemetryData { get; set; }
    }
}
