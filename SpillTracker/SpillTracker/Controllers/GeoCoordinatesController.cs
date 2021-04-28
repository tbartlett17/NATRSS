using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpillTracker.Models;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace SpillTracker.Controllers
{
    public class GeoCoordinatesController : Controller
    {

        private readonly IConfiguration _config;



        public GeoCoordinatesController(IConfiguration config)
        {
            _config = config;

        }
        
        // POST: GeoCoordinatesController/Create
        [HttpPost]
        [AllowAnonymous]
        public IActionResult GetCoords(string streetAddress)
        {

            /*StreetAddress test = new StreetAddress { Street = "1120 18th st se", City = "Salem", State = "OR", PostalCode = "97302" };*/
            StreetAddress newStreetAddress = JsonConvert.DeserializeObject<StreetAddress>(streetAddress);

            string secret = _config["AJAX1:MapQuestAPIKey"];
            string url = $"http://www.mapquestapi.com/geocoding/v1/address?key={secret}&street={newStreetAddress.Street}&city={newStreetAddress.City}&state={newStreetAddress.State}&postalCode={newStreetAddress.PostalCode}";
            

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
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

                //The API call worked correctly now update the cid and molecular weight
                Debug.WriteLine(jsonString);
                JObject geo = JObject.Parse(jsonString);
                GeoLocationCoordinates theCoordinates = new GeoLocationCoordinates();

                theCoordinates.Longitude = (string)geo["results"][0]["locations"][0]["latLng"]["lng"];
                theCoordinates.Latitude = (string)geo["results"][0]["locations"][0]["latLng"]["lat"];              
                var jsonCoordinates = JsonConvert.SerializeObject(theCoordinates);
                
                return Json(jsonCoordinates);
            }
            catch
            {
                return StatusCode(500); ;
            }
        }

    }
}
