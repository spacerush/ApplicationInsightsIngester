using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Collector.Models;
using Collector.Models.ViewModels;
using Collector.Services;

namespace Collector.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomTelemetryService service;

        public HomeController(ICustomTelemetryService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel(service);
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
