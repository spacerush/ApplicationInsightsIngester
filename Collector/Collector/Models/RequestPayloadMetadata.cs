using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Models
{
    public class RequestPayloadMetadata
    {
        public string Level { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}
