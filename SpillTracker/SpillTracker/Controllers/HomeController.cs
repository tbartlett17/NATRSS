using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpillTracker.Models;

namespace SpillTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly SpillTrackerDbContext db;

        Home mystatus = new Home();

        public HomeController(ILogger<HomeController> logger, SpillTrackerDbContext context)
        {
            _logger = logger;
            db = context;
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

       /* public IActionResult twoXeight() 
        {
            int x = 8;
            int y ;
            return View();
        }*/



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
