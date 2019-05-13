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
        private readonly ITelemetryRetrievalService telementryRetrievalService;
        public List<TelemetryKey> TelemetryKeys { get; set; }

        public ShowKeysViewModel(ITelemetryRetrievalService telementryRetrievalService)
        {
            this.telementryRetrievalService = telementryRetrievalService;
            this.TelemetryKeys = this.telementryRetrievalService.GetAllTelemetryKeys();

        }
    }
}
