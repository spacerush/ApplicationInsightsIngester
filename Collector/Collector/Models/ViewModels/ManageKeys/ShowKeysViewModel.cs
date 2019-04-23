using Collector.Models.Documents;
using Collector.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Models.ViewModels.ManageKeys
{
    public class ShowKeysViewModel
    {
        private readonly ICustomTelemetryService customTelemetryService;
        public List<TelemetryKey> TelemetryKeys { get; set; }

        public ShowKeysViewModel(ICustomTelemetryService customTelemetryService)
        {
            this.customTelemetryService = customTelemetryService;
            this.TelemetryKeys = this.customTelemetryService.GetAllTelemetryKeys();

        }
    }
}
