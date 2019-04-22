using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Models
{
    public class AggregateDependencyDuration
    {
        public string ApplicationId { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public decimal Sum { get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public int Count { get; set; }
        public decimal StandardDeviation { get; set; }
        public string DependencyType { get; set; }
    }
}
