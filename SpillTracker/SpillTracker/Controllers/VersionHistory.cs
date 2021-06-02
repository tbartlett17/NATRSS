using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using SpillTracker.Models;

namespace SpillTracker.Controllers
{
    [AllowAnonymous]
    public class VersionHistory : Controller
    {
        private readonly IConfiguration _config;

        public VersionHistory(IConfiguration config)
        {
            _config = config;
        }
        public IActionResult Index()
        {
            string request = SendRequest("https://api.github.com/repos/NickApa/NATRSS/commits", "NickApa");
            var data = JArray.Parse(request);
            ListOfCommitsVM listOfCommitsVM = new ListOfCommitsVM();
            foreach (var element in data)
            {
                Commit c = new Commit();
                c.commitId = (string)element["sha"];
                c.commitId = c.commitId.Substring(0, 7);
                c.commitLink = (string)element["html_url"];
                c.commitMessage = (string)element["commit"]["message"];                
                c.date = (string)element["commit"]["committer"]["date"];
                c.date = c.date.Substring(0, 16);
                if (c.commitMessage.Contains("Merge"))
                {

                }
                else
                {
                    listOfCommitsVM.commits.Add(c);
                }
            }
            /*return Json(commitList);*/
            return View(listOfCommitsVM);
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
    }
}
