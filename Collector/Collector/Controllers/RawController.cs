﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Collector.Models;
using Collector.Services;
using Collector.Models.ViewModels;
using Collector.Models.ServiceResponse;
using CookieManager;
using Collector.Models.ViewModels.Raw;

namespace Collector.Controllers
{
    public class RawController : Controller
    {
        private readonly ICustomTelemetryService service;
        private readonly IAuthenticationService authenticationService;
        private readonly ICookie cookie;
        private readonly ICookieManager cookieManager;

        public RawController(ICustomTelemetryService service, IAuthenticationService authenticationService, ICookie cookie, ICookieManager cookieManager)
        {
            this.service = service;
            this.authenticationService = authenticationService;
            this.cookie = cookie;
            this.cookieManager = cookieManager;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel(service);
            return View(viewModel);
        }

        /// <summary>
        /// Show telemetry reported within the last hour
        /// </summary>
        public IActionResult LastHour()
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
                    var viewModel = new LastHourRawViewModel(service);
                    return View(viewModel);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }

        }

        /// <summary>
        /// Show all telemetry reported within the last day
        /// </summary>
        public IActionResult LastDay()
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
                    var viewModel = new LastDayRawViewModel(service);
                    return View(viewModel);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }

        }

        /// <summary>
        /// Show all telemetry reported within the last day
        /// </summary>
        public IActionResult Latest()
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
                    var viewModel = new LatestRawViewModel(service);
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
