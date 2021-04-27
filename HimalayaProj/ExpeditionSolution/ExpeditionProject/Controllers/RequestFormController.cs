using ExpeditionProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public IActionResult Index(int? uid, int? eid)
        {       
            ViewData["ThisCurrentUser"] = uid;
            ViewData["ThisCurrentExpedition"] = eid;
            return View();
        }

        [HttpPost]
        public IActionResult Index(Form theForm)
        {


            User theUser = _context.Users.Where(i => i.Id == theForm.UserId).FirstOrDefault();

            if (ModelState.IsValid)
            {
                _context.Add(theForm);
                _context.SaveChanges();
                
                return RedirectToAction("Account", "Login", new { id = theForm.UserId });
            }

            Debug.WriteLine("failed here");
            return RedirectToAction(nameof(Index));
        }
        public IActionResult List(int? id)
        {
            
            User theUser = _context.Users.Where(i => i.Id == id).Include(u => u.UserType).FirstOrDefault();
            IList<Form> FormsDbContext = new List<Form>();

            if (theUser.UserTypeId == 2)
            {
                FormsDbContext = _context.Forms.Where(i=>i.UserId==id).OrderByDescending(x => x.SubmissionDateTime).Include(e => e.Expedition).Include(e => e.User).ToList();
            }
            else
            { 
               FormsDbContext = _context.Forms.OrderByDescending(x => x.SubmissionDateTime).Include(e => e.Expedition).Include(e => e.User).ToList();
            }
            userAndFormArrayVM uafavm = new userAndFormArrayVM
            {

                thisUser = theUser,
                thisFormArray = FormsDbContext,
            };

            return View(uafavm);
        }

        public IActionResult RequestFormReview(int id, int uid)
        {

            User theUser = _context.Users.Where(i => i.Id == uid).Include(u => u.UserType).FirstOrDefault();
            Form theForm = _context.Forms.Where(x => x.Id == id).Include(e => e.Expedition).Include(e => e.User).FirstOrDefault();

            userAndFormReviewVM uafrvm = new userAndFormReviewVM
            {

                thisUser = theUser,
                thisForm = theForm,
            };

            return View(uafrvm);

        }

       
    }
}
