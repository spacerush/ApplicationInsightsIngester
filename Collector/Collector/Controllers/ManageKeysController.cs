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
using Collector.Models.ViewModels.ManageKeys;

namespace Collector.Controllers
{
    public class ManageKeysController : Controller
    {
        private readonly ICustomTelemetryService customTelemetryService;
        private readonly IAuthenticationService authenticationService;
        private readonly ICookie cookie;
        private readonly ICookieManager cookieManager;
        private readonly ITelemetryRetrievalService telemetryRetrievalService;

        public ManageKeysController(ICustomTelemetryService customTelemetryService, ITelemetryRetrievalService telemetryRetrievalService, IAuthenticationService authenticationService, ICookie cookie, ICookieManager cookieManager)
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
        /// Show metrics reported within the last hour
        /// </summary>
        [HttpGet]
        [HttpPost]
        public IActionResult ShowKeys(string newAppId, string expireKey)
        {
            string sessionId = this.cookie.Get("TelemetrySession");
            if (string.IsNullOrEmpty(sessionId))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                GetUserByCookieResponse reportUserByCookie = this.authenticationService.GetUserByWebCookie(sessionId);
                if (reportUserByCookie.Success == true)
                {
                    if (reportUserByCookie.User.IsOrganizationAdmin)
                    {
                        if (!(string.IsNullOrEmpty(expireKey))) {
                            this.customTelemetryService.ExpireTelemetryKey(Guid.Parse(expireKey));
                        }
                        if (!(string.IsNullOrEmpty(newAppId))) {
                            this.customTelemetryService.AddTelemetryKey(newAppId, reportUserByCookie.User.Username);
                        }
                    }
                    var viewModel = new ShowKeysViewModel(telemetryRetrievalService);
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
