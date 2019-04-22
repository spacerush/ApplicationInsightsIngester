using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Models.Documents
{
    public class TelemetryKey : Document
    {
        public string UsernameWhoAdded { get; set; }
        public string ApplicationId { get; set; }
        public string KeyData { get; set; }
        public bool Expired { get; set; }
        public string ExpireReason { get; set; }
    }
}
