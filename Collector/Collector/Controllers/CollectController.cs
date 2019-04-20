using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Collector.Models;
using Microsoft.AspNetCore.Mvc;

namespace Collector.Controllers
{
    // see https://weblog.west-wind.com/posts/2017/sep/14/accepting-raw-request-body-content-in-aspnet-core-api-controllers
    public class CollectController : Controller
    {
        [HttpPost]
        [HttpGet]
        [Route("api/Collect")]
        public IActionResult Collect([FromBody] string content)
        {
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