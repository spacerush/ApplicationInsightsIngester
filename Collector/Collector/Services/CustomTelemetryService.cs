using Collector.EFModel;
using Collector.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Services
{
    public class CustomTelemetryService : ICustomTelemetryService
    {
        private readonly IRepoWrapper repoWrapper;

        public CustomTelemetryService(IRepoWrapper repositoryWrapper)
        {
            this.repoWrapper = repositoryWrapper;
        }

        public void RecordTelemetry(string telemetry)
        {
            TelemetryLog telemetryLog = new TelemetryLog();
            telemetryLog.TelemetryData = telemetry;
            this.repoWrapper.TelemetryLogRepository.Create(telemetryLog);
            this.repoWrapper.Save();
        }
    }
}
