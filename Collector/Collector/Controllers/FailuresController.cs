using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Collector.Models;
using Collector.Services;
using Collector.Models.ViewModels.Dependencies;
using Collector.Models.ViewModels;
using Collector.Models.ServiceResponse;
using CookieManager;
using Collector.Models.ViewModels.Failures;

namespace Collector.Controllers
{
    public class FailuresController : Controller
    {
        private readonly ICustomTelemetryService customTelemetryService;
        private readonly ITelemetryRetrievalService telemetryRetrievalService;
        private readonly IAuthenticationService authenticationService;
        private readonly ICookie cookie;
        private readonly ICookieManager cookieManager;

        public FailuresController(ITelemetryRetrievalService telemetryRetrievalService, ICustomTelemetryService customTelemetryService, IAuthenticationService authenticationService, ICookie cookie, ICookieManager cookieManager)
        {
            this.customTelemetryService = customTelemetryService;
            this.telemetryRetrievalService = telemetryRetrievalService;
            this.authenticationService = authenticationService;
            this.cookie = cookie;
            this.cookieManager = cookieManager;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel(telemetryRetrievalService);
            return View(viewModel);
        }

        /// <summary>
        /// Show all telemetry reported within the last day
        /// </summary>
        public IActionResult Today()
        {
            string sessionId = this.cookie.Get("TelemetrySession");
            if (string.IsNullOrEmpty(sessionId))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                GetUserByCookieResponse reportUserByCookie = this.authenticationService.GetUserByWebCookie(sessionId);
                if (reportUserByCookie.Success == true && reportUserByCookie.User.IsOrganizationAdmin)
                {
                    var viewModel = new LastDayFailuresViewModel(telemetryRetrievalService);
                    return View(viewModel);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }

        }

    }
}
