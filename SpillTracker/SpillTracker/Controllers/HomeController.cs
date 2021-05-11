using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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
            string secret = _config["NatrGitkey"];
            Debug.WriteLine("secret " + secret);
            return View();
        }



        [HttpGet]
        public JsonResult versionHistory()
        {
            //var tmp = "this is from version history";
            string request = SendRequest("https://api.github.com/repos/NickApa/NATRSS/commits", "NickApa");            
            var data = JArray.Parse(request);
            List<Commits> commitList = new List<Commits>();
            foreach (var element in data)
            {
                Commits c = new Commits();
                c.commitId = (string)element["sha"];
                c.commitId = c.commitId.Substring(0, 7);
                c.commitMessage = (string)element["commit"]["message"];
                c.date = (string)element["commit"]["committer"]["date"];
                c.date = c.date.Substring(0, 16);
                commitList.Add(c);
            }
            return Json(commitList);
        }

        private string SendRequest(string uri, string username)
        {
            string secret = _config["NatrGitkey"];
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Headers.Add("Authorization", "token " + secret);
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
