using Collector.Models;
using Collector.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Services
{
    public class CustomTelemetryService : ICustomTelemetryService
    {
        private readonly IRepositoryWrapper repositoryWrapper;

        public CustomTelemetryService(IRepositoryWrapper repositoryWrapper)
        {
            this.repositoryWrapper = repositoryWrapper;
        }

        public void RecordTelemetry(string telemetry, string applicationId)
        {
            TelemetryContainer telemetryContainer = new TelemetryContainer();
            telemetryContainer.TelemetryData = telemetry;
            telemetryContainer.UtcDate = DateTime.UtcNow;
            telemetryContainer.ApplicationId = applicationId;
            this.repositoryWrapper.TelemetryRepository.AddOne<TelemetryContainer>(telemetryContainer);
        }
    }
}
