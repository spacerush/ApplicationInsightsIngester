using System;
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
    public class ChartsController : Controller
    {
        private readonly ICustomTelemetryService service;
        private readonly IAuthenticationService authenticationService;
        private readonly ICookie cookie;
        private readonly ICookieManager cookieManager;

        public ChartsController(ICustomTelemetryService service, IAuthenticationService authenticationService, ICookie cookie, ICookieManager cookieManager)
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
        /// Show telemetry reported within the day
        /// </summary>
        public IActionResult Daily()
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
                    var viewModel = new DailyChartViewModel(service);
                    return View(viewModel);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
        }

        /// <summary>
        /// Show telemetry reported within the week
        /// </summary>
        public IActionResult Weekly()
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
                    var viewModel = new WeeklyChartViewModel(service);
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
