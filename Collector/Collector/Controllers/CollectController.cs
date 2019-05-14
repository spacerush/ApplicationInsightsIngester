using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Collector.Hubs;
using Collector.Models;
using Collector.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Collector.Controllers
{
    // see https://weblog.west-wind.com/posts/2017/sep/14/accepting-raw-request-body-content-in-aspnet-core-api-controllers
    public class CollectController : Controller
    {
        private readonly ITelemetryRetrievalService telemetryRetrievalService;
        private readonly ICustomTelemetryService customTelemetryService;
        private readonly IHubContext<TelemetryHub, IJavascriptClient> telemetryHubContext;
        public CollectController(ICustomTelemetryService telemetryService, IHubContext<TelemetryHub, IJavascriptClient> telemetryHubContext, ITelemetryRetrievalService telemetryRetrievalService)
        {
            this.customTelemetryService = telemetryService;
            this.telemetryHubContext = telemetryHubContext;
            this.telemetryRetrievalService = telemetryRetrievalService;
        }

        [HttpPost]
        [HttpGet]
        [Route("api/Collect")]
        public IActionResult Collect([FromHeader] string appId, [FromHeader] string appKey)
        {
            if (this.customTelemetryService.CheckTelemetryKey(appId, appKey))
            {
                string requestbody = Request.GetRawBodyStringAsync().Result;
                Guid telemetryId = this.customTelemetryService.RecordTelemetry(requestbody, appId);
                List<RequestPayload> payloads = telemetryRetrievalService.GetRequestPayloadById(telemetryId);
                telemetryHubContext.Clients.All.ReceiveMessage(new Dto.MessageEnvelope(payloads.Count.ToString() + " request payloads delivered to telemetry service."));
                foreach (var item in payloads)
                {
                    telemetryHubContext.Clients.All.ReceiveMessage(new Dto.MessageEnvelope(item.TelemetryApplicationId + " responded with " + item.ResponseCode + " to a request that took " + item.Duration.TotalMilliseconds + " ms to complete on server " + item.ServerName));
                    telemetryHubContext.Clients.All.ReceiveDatapoint(new Dto.AppDatapoint(item.ServerName, item.TelemetryApplicationId, item.Name, item.Duration.TotalMilliseconds));
                }
                var acceptedResponse = new AcceptedResponse();
                acceptedResponse.itemsAccepted = 1;
                acceptedResponse.itemsReceived = 1;
                acceptedResponse.errors = new List<string>();
                var result = new JsonResult(acceptedResponse);
                result.StatusCode = 200;
                return result;
            }
            else
            {
                string requestbody = Request.GetRawBodyStringAsync().Result;
                this.customTelemetryService.LogRejectedTelemetry(appId, appKey, requestbody, "Application key incorrect");
                var acceptedResponse = new AcceptedResponse();
                acceptedResponse.itemsAccepted = 0;
                acceptedResponse.itemsReceived = 0;
                acceptedResponse.errors = new List<string>();
                acceptedResponse.errors.Add("Cannot process due to incorrect key.");
                var result = new JsonResult(acceptedResponse);
                result.StatusCode = 403;
                return result;
            }
        }
    }
}