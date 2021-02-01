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


            User theUser = _context.Users.Where(i => i.Id == theForm.UserId).FirstOrDefault();


            return RedirectToAction("Index", "Login", theUser);
        }
        public IActionResult List()
        {

            var FormsDbContext = _context.Forms.OrderByDescending(x => x.SubmissionDateTime).Include(e => e.Expedition).Include(e => e.User).Take(50);


            return View(FormsDbContext);
        }

        public IActionResult RequestFormReview(int id)
        {

            Form theForm = _context.Forms.Where(x => x.Id == id).Include(e => e.Expedition).Include(e => e.User).FirstOrDefault();


            return View(theForm);

        }

        [HttpPost]
        public IActionResult RequestFormReview([Bind("Id,Description,Status,Completed,ExpedtionId,UserId,SubmissionDateTime")] Form theForm)
        {




            return View();
        }

    }
}
