using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Models
{
    public class TelemetryMetadata
    {
        public string ApplicationId { get; set; }
        public DateTimeOffset DateTimeOffset { get; set; }
        public int TelemetryLength { get; set; }
    }
}
