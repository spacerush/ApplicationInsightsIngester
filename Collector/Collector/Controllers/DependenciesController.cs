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

namespace Collector.Controllers
{
    public class DependenciesController : Controller
    {
        private readonly ICustomTelemetryService customTelemetryService;
        private readonly ITelemetryRetrievalService telemetryRetrievalService;
        private readonly IAuthenticationService authenticationService;
        private readonly ICookie cookie;
        private readonly ICookieManager cookieManager;

        public DependenciesController(ICustomTelemetryService customTelemetryService, ITelemetryRetrievalService telemetryRetrievalService, IAuthenticationService authenticationService, ICookie cookie, ICookieManager cookieManager)
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
        public IActionResult LastHourMetrics()
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
                    var viewModel = new LastHourMetricsViewModel(telemetryRetrievalService);
                    return View(viewModel);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }

        }

        /// <summary>
        /// Show metrics reported within the last day
        /// </summary>
        public IActionResult LastDayMetrics()
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
                    var viewModel = new LastDayMetricsViewModel(telemetryRetrievalService);
                    return View(viewModel);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
        }

        /// <summary>
        /// Show most recent metrics reported
        /// </summary>
        public IActionResult LatestMetrics()
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
                    var viewModel = new LatestMetricsViewModel(telemetryRetrievalService);
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
