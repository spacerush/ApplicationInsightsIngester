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

        public void RecordTelemetry(string telemetry, string applicationId)
        {
            TelemetryLog telemetryLog = new TelemetryLog();
            telemetryLog.TelemetryData = telemetry;
            telemetryLog.UtcDate = DateTime.UtcNow;
            telemetryLog.ApplicationId = applicationId;
            this.repoWrapper.TelemetryLogRepository.Create(telemetryLog);
            this.repoWrapper.Save();
        }
    }
}
