using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ExpeditionProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpeditionProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HimalayasDbContext _context;
        public HomeController(ILogger<HomeController> logger, HimalayasDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }

        public IActionResult Stats()
        {

            List<string> mostPopularPeaksCopy = new List<string>();
            var mostPopularPeakID = _context.Expeditions.Include(p => p.Peak)
                                    .GroupBy(q => q.PeakId)
                                    .OrderByDescending(gp => gp.Count())
                                    .Select(a => a.Key)
                                    .Take(5)
                                    .ToArray();

            foreach (int element in mostPopularPeakID)           
            {
                mostPopularPeaksCopy.Add(_context.Peaks.Where(p => p.Id == element).FirstOrDefault().Name);
            }


            statsVM theStats = new statsVM()
            {

                currentNumberOfExpeditions = _context.Expeditions.Count(),
                currentNumberOfPeaks = _context.Peaks.Count(),
                numberOfUnClimbedPeaks = _context.Peaks.Where(e => e.FirstAscentYear == null).Count(),

                mostPopularPeaks = mostPopularPeaksCopy,
                peaksHigherThan7000 = _context.Peaks.Where(a=>a.Height > 7000).Select(g => g.Name).ToList(),
                peaksHigherThan6000 = _context.Peaks.Where(a => a.Height > 6000 && a.Height < 7000 ).Select(g => g.Name).ToList(),

            };
        

       

            return View(theStats);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult FetchStats()
        {
            // need to return total # of expeditons, total number of peaks, number of climbed peaks

            int totalNumExps = _context.Expeditions.Count();
            int totalNumPeaks = _context.Peaks.Count();
            int numPeaksClimbed = _context.Peaks.Where(p => p.ClimbingStatus == true).Count();

            string stats = " \"Total Expeditions\": " + totalNumExps + ", \"Number of Peaks\": " + totalNumPeaks + ", \"Number of peaks climbed\": " + numPeaksClimbed ;

            return Json(stats);
        }
    }
}
