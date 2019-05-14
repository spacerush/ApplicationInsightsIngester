using Collector.Models;
using Collector.Models.Documents;
using Collector.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public Guid RecordTelemetry(string telemetry, string applicationId)
        {
            TelemetryContainer telemetryContainer = new TelemetryContainer();
            telemetryContainer.TelemetryData = telemetry;
            telemetryContainer.UtcDate = DateTime.UtcNow;
            telemetryContainer.ApplicationId = applicationId;
            this.repositoryWrapper.TelemetryRepository.AddOne<TelemetryContainer>(telemetryContainer);
            return telemetryContainer.Id;
        }

        

        public void AddTelemetryKey(string applicationId, string username)
        {
            var newKey = new TelemetryKey();
            newKey.ApplicationId = applicationId;
            newKey.Expired = false;
            newKey.ExpireReason = null;
            newKey.KeyData = Helpers.GenerationHelper.CreateRandomString(true, true, false, 20);
            newKey.UsernameWhoAdded = username;
            this.repositoryWrapper.TelemetryKeyRepository.AddOne<TelemetryKey>(newKey);
        }

        public bool CheckTelemetryKey(string applicationId, string keyData)
        {
            bool result = false;
            var matches = this.repositoryWrapper.TelemetryKeyRepository.GetAll<TelemetryKey>(f => f.ApplicationId == applicationId && f.KeyData == keyData && f.Expired == false).ToList();
            if (matches.Count > 0)
            {
                result = true;
            }
            return result;
        }

        public void ExpireTelemetryKey(Guid keyId)
        {
            var match = this.repositoryWrapper.TelemetryKeyRepository.GetOne<TelemetryKey>(f => f.Id == keyId);
            match.Expired = true;
            this.repositoryWrapper.TelemetryKeyRepository.UpdateOne<TelemetryKey>(match);
        }

        public void LogRejectedTelemetry(string applicationId, string keyData, string telemetryData, string reason)
        {
            var reject = new RejectedTelemetry();
            reject.ApplicationId = applicationId;
            reject.Key = keyData;
            reject.TelemetryData = telemetryData;
            reject.RejectionReason = reason;
            this.repositoryWrapper.RejectedTelemetryRepository.AddOne<RejectedTelemetry>(reject);
        }


    }
}
