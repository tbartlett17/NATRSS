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

    public class LoginController : Controller 
    {
        private readonly HimalayasDbContext _context;

        public LoginController(HimalayasDbContext context)
        {
            _context = context;
        }

        //get 
        public IActionResult Index() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            if (_context.Users.Any(u => u.UserName == user.UserName)) //is username in db?
            {
                User thisUser = _context.Users.Where(u => u.UserName == user.UserName).FirstOrDefault();
                if (thisUser.Password == user.Password) //does password enter match users password in db?
                {
                    Debug.WriteLine("\n\nuser login success\n\n");
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                else //incorrect password
                {
                    Debug.WriteLine("\n\n password incorrect\n\n");
                    ModelState.AddModelError("Error", "invalid username or password");
                    return View();
                }
            }
            else //user not found in db
            {
                Debug.WriteLine("\n\n user not found\n\n");
                ModelState.AddModelError("Error", "invalid username or password");
                return View();
            }
        }


        public IActionResult Register()
        {
            ViewData["UserTypes"] = new SelectList(_context.UserTypes.Where(ut => ut.Role != "Employee").OrderBy(ut => ut.Id), "Id", "Role").Reverse();
            
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                User newUser = new User
                {
                    UserName = user.UserName,
                    Password = user.Password,
                    Name = user.Name,
                    BirthDate = user.BirthDate,
                    UserTypeId = user.UserTypeId
                };
                _context.Users.Add(newUser);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            else
            {
                return View();
            }
     
        }

    }
    

}