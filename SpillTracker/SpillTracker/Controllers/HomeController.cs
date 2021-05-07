using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpillTracker.Models;

namespace SpillTracker.Controllers
{
    [AllowAnonymous]
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

        [HttpGet]
        public JsonResult versionHistory()
        {
            var tmp = "this is from version history";
            var request = SendRequest("https://github.com/NickApa/NATRSS", "Nicks Token", "NickApa");
            // create a class called commit then define the info I wanna take out then create a list of type commit
            // then create a for loop to go through every commit in the request to then make new commit objects from those commits and add it to the list
            // then return the list
            //var 



            return Json(tmp);

            //return Json([whatever data you're trying to return], JsonRequestBehavior.AllowGet);
        }

        private string SendRequest(string uri, string credentials, string username)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Headers.Add("Authorization", "token " + credentials);
            request.UserAgent = username;       // Required, see: https://developer.github.com/v3/#user-agent-required
            request.Accept = "application/json";

            string jsonString = null;
            // TODO: You should handle exceptions here
            using (WebResponse response = request.GetResponse())
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                jsonString = reader.ReadToEnd();
                reader.Close();
                stream.Close();
            }
            return jsonString;
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
