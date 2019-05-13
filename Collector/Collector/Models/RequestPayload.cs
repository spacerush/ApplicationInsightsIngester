using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Models
{
    public class RequestPayload
    {
        public RequestPayloadMetadata Metadata { get; set; }
        public string TelemetryType { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public string Id { get; set; }
        public string ResponseCode { get; set; }
        public bool Success { get; set; }
        public string Url { get; set; }
        public string DeveloperMode { get; set; }
        public string ai_component_version { get; set; }
        public string ai_cloud_role_instance { get; set; }
        public string ai_operation_id { get; set; }
        public string ai_operation_name { get; set; }
        public string ai_location_ip { get; set; }
        public string ai_DeveloperMode { get; set; }
        public string ServerName { get; set; }
    }
}
