﻿using Collector.Models;
using Collector.Models.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Services
{
    public interface ICustomTelemetryService
    {
        Guid RecordTelemetry(string telemetry, string applicationId);

        void AddTelemetryKey(string applicationId, string username);

        bool CheckTelemetryKey(string applicationId, string keyData);

        void ExpireTelemetryKey(Guid keyId);

        void LogRejectedTelemetry(string applicationId, string keyData, string telemetryData, string reason);


    }
}
