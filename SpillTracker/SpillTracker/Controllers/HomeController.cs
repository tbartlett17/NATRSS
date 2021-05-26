using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SpillTracker.Models;

namespace SpillTracker.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly SpillTrackerDbContext db;

        private readonly IConfiguration _config;

        Home mystatus = new Home();

        public HomeController(ILogger<HomeController> logger, SpillTrackerDbContext context, IConfiguration config)
        {
            _logger = logger;
            db = context;
            _config = config;
        }

        public IActionResult Index()
        {
            mystatus.chemicals = db.Chemicals;
            mystatus.times = db.StatusTimes;
            return View(mystatus);
        }

        public IActionResult Guide() 
        {
            return View();
        }

        public IActionResult Disclaimer() 
        {

            return View();
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
