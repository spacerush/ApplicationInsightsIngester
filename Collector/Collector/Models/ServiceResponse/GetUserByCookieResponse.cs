using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Models.ServiceResponse
{
    public class GetUserByCookieResponse
    {
            public CollectorUser User { get; set; }
            public bool Success { get; set; }
    }
}
