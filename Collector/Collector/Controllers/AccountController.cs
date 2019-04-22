using CookieManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Collector.Services;
using Collector.Models;
using Collector.Models.ServiceResponse;
using Collector.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Collector.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAuthenticationService _authenticationService;
        private readonly ICookie cookie;
        private readonly ICookieManager cookieManager;

        public AccountController(IAuthenticationService authenticationService, ICookie cookie, ICookieManager cookieManager)
        {
            _authenticationService = authenticationService;
            this.cookie = cookie;
            this.cookieManager = cookieManager;
        }

        public IActionResult Index()
        {
            var viewModel = new AccountIndexViewModel();
            string sessionId = cookie.Get("TelemetrySession");
            if (string.IsNullOrEmpty(sessionId))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                GetUserByCookieResponse reportUserByCookie = _authenticationService.GetUserByWebCookie(sessionId);
                if (reportUserByCookie.Success == true)
                {
                    viewModel.Message = "Welcome, " + reportUserByCookie.User.Username;
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            return View(viewModel);
        }
        public IActionResult Login()
        {
            cookie.Remove("TelemetrySession");
            return View();
        }

        public IActionResult Logout()
        {
            cookie.Remove("TelemetrySession");
            return View();
        }

        [HttpPost]
        public IActionResult DoLogin(string spusername, string sppassword)
        {
            var viewModel = new DoLoginViewModel();

            if (_authenticationService.TryLoginCredentials(spusername, sppassword))
            {
                WebSession session = _authenticationService.CreateWebSession(spusername);
                viewModel.Message = "Created new web session valid until " + session.Expiry.ToShortDateString();
                cookie.Set("TelemetrySession", session.SessionCookie, new CookieOptions() { HttpOnly = true, Expires = DateTime.UtcNow.AddDays(13) });
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
