using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Dto
{
    public class AppDatapoint
    {
        public AppDatapoint(string serverName, string applicationId, string actionName, double durationMs)
        {
            this.DurationMs = durationMs;
            this.ApplicationId = applicationId;
            this.ServerName = serverName;
            this.ActionName = actionName;
        }
        public string ServerName { get; set; }
        public string ApplicationId { get; set; }
        public string ActionName { get; set; }
        public double DurationMs { get; set; }
    }
}
