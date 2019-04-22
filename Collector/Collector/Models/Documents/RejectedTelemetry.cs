using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Models.Documents
{
    public class RejectedTelemetry : MongoDbGenericRepository.Models.Document
    {
        public string ApplicationId { get; set; }
        public string Key { get; set; }
        public string TelemetryData { get; set; }
        public string RejectionReason { get; set; }
    }
}
