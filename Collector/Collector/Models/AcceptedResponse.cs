using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Models
{
    public class AcceptedResponse
    {
        public int itemsReceived { get; set; }
        public int itemsAccepted { get; set; }
        public List<string> errors;

        public AcceptedResponse()
        {

        }
    }
}
