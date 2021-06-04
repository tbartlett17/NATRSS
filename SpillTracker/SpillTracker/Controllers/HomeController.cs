using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SendGrid;
using SendGrid.Helpers.Mail;
using SpillTracker.Models;
using SpillTracker.Services;

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

        [Authorize(Roles = "Admin,Employee,FacilityManager")]
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Contact(string subject, string body)
        {
            Debug.WriteLine(subject + "\n" + body);
            SendTheEmail(subject, body);
            return View();
        }

        private void SendTheEmail(string subject, string body)
        {
            var apiKey = _config["SendGridKey"]; //SendGridKey
            var client = new SendGridClient(apiKey);
            /*Debug.WriteLine("apiKey " + apiKey);*/
            var from = new EmailAddress("rljohns579@gmail.com", "User#1");
            var htmlContent = "";
            var to = new EmailAddress("natrss@protonmail.com", "Team NATR");
            var msg = MailHelper.CreateSingleEmail(from, to, subject, body, htmlContent);
            var response = client.SendEmailAsync(msg);
        }


        public IActionResult Disclaimer()
        {

            return View();
        }

        public IActionResult Credit()
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
