using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Collector.Models;
using Collector.Services;
using Microsoft.AspNetCore.Mvc;

namespace Collector.Controllers
{
    // see https://weblog.west-wind.com/posts/2017/sep/14/accepting-raw-request-body-content-in-aspnet-core-api-controllers
    public class CollectController : Controller
    {

        private readonly ICustomTelemetryService customTelemetryService;

        public CollectController(ICustomTelemetryService telemetryService)
        {
            this.customTelemetryService = telemetryService;
        }

        [HttpPost]
        [HttpGet]
        [Route("api/Collect")]
        public IActionResult Collect([FromHeader] string appId)
        {
            string requestbody = Request.GetRawBodyStringAsync().Result;
            this.customTelemetryService.RecordTelemetry(requestbody, appId);

            var acceptedResponse = new AcceptedResponse();
            acceptedResponse.itemsAccepted = 1;
            acceptedResponse.itemsReceived = 1;
            acceptedResponse.errors = new List<string>();
            var result = new JsonResult(acceptedResponse);
            result.StatusCode = 200;
            return result;
        }
    }
}