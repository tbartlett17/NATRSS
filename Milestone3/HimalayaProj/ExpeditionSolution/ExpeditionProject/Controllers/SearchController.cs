using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpeditionProject.Models;
using System.Diagnostics;

namespace ExpeditionProject.Controllers
{

    public class SearchController : Controller 
    {
        private readonly HimalayasDbContext _context;

        public SearchController(HimalayasDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() 
        {
            return View();
        }

        [HttpGet]
        public IActionResult searchPeaks()
        {
            ViewBag.Success = false;
            
            return View();
        }

        [HttpPost]
        public IActionResult searchPeaks(string inputString)
        {

            var peakList = _context.Peaks.Where(x => x.Name.Contains(inputString)).ToList();
            //peakList =
            /*for (int i = 0; i < peakList.Count(); i++) 
            {
                Debug.WriteLine(peakList[i].Name);
            }*/
            //Debug.WriteLine("This was the search of " + inputString);
            ViewBag.Success = true;
            ViewBag.peakList = peakList;

            return View();
        }

        /*[HttpGet]
        public IActionResult searchDeets(int? id) 
        {
            Peak OGpeak = _context.Peaks.Find(id);
            Peak peak1 = new Peak(); // new model
            peak1.Name = OGpeak.Name;
            var temp = _context.Peaks.Where(x => x.Id == id).ToList();
            //
            for (int i = 0; i < temp.Count(); i++) 
            {
                Peak peak = new Peak();
                peak.Name = temp[i].Name;
                peak.Id = temp[i].Id;
            } 

            return View();
        }*/

    }
    

}