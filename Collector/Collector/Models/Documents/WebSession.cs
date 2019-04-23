using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Models.Documents
{
    public class WebSession : MongoDbGenericRepository.Models.Document
    {
        public Guid ReportUserId { get; set; }
        public string SessionCookie { get; set; }
        public string ForReportUsername { get; set; }
        public DateTime Expiry { get; set; }
    }
}
