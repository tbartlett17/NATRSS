using ExpeditionProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpeditionProject.Controllers
{
    public class RequestFormController : Controller
    {

        private readonly HimalayasDbContext _context;

        public RequestFormController(HimalayasDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Form theForm)
        {
            
            
            return RedirectToAction("Index", "Login");
        }
        public IActionResult List()
        {

            var FormsDbContext = _context.Forms.OrderByDescending(x => x.SubmissionDateTime).Include(e => e.Expedition).Include(e => e.User).Take(50);
            

            return View(FormsDbContext);
        }

        public IActionResult RequestFormReview(int id)
        {

            var FormsDbContext = _context.Forms.OrderByDescending(x => x.SubmissionDateTime).Include(e => e.Expedition).Include(e => e.User).Take(50);


            return View(FormsDbContext);

        }

        [HttpPost]
        public IActionResult RequestFormReview(Form theForm)
        {

            var FormsDbContext = _context.Forms.OrderByDescending(x => x.SubmissionDateTime).Include(e => e.Expedition).Include(e => e.User).Take(50);


            return View(FormsDbContext);
        }

    }
}
