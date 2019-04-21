using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Services
{
    public interface ICustomTelemetryService
    {
        void RecordTelemetry(string telemetry);
    }
}
