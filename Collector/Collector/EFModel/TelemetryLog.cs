﻿using System;
using System.Collections.Generic;

namespace Collector.EFModel
{
    public partial class TelemetryLog
    {
        public long TelemetryLogId { get; set; }
        public string ApplicationId { get; set; }
        public DateTime UtcDate { get; set; }
        public string TelemetryData { get; set; }
    }
}