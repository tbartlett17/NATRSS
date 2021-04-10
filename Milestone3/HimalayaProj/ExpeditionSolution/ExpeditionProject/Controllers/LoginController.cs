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
        public IActionResult Index( ) 
        {
           

            return View();
        }

       

        [HttpPost]
        public IActionResult Index(User user)
        {
            if (_context.Users.Any(u => u.UserName == user.UserName)) //is username in db?
            {
                User loggedInUser = _context.Users.Include(ut => ut.UserType).Where(u => u.UserName == user.UserName).FirstOrDefault();
                if (loggedInUser.Password == user.Password) //does password enter match users password in db?
                {
                    Debug.WriteLine("\n\nuser login success\n\n");
                    //return RedirectToAction("Index", "Login", loggedInUser);

                    AccountVM account = new AccountVM();
                    account.User = loggedInUser;

                    if (account.User.UserType.Role == "Expedition Provider")
                    {
                        account.UsersExpeditions = _context.Expeditions.Where(e => e.TrekkingAgencyId == 3);
                    };



                    //return View(account);
                    return RedirectToAction("Account", "Login", new { id = account.User.Id });
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

        public IActionResult Account(int id)
        {
            AccountVM thisAccount = new AccountVM();
            thisAccount.User = _context.Users.Include(ut => ut.UserType).Where(u => u.Id == id).FirstOrDefault();
            thisAccount.UsersExpeditions = _context.Expeditions.Include(e => e.Peak).Include(e => e.TrekkingAgency).Where(e => e.TrekkingAgencyId == 3);

            return View(thisAccount);
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

                AccountVM newAccount = new AccountVM();
                newAccount.User = newUser;
                newAccount.UsersExpeditions = null;

                return RedirectToAction("Account", "Login", new { id = newAccount.User.Id });
            }
            else
            {
                return View();
            }
     
        }

        [HttpPost]
        public IActionResult RequestModification(int id)
        {


            return RedirectToAction("Index", "Home");
            //return RedirectToAction("Index", "RequestForm", new { id = 3 });
        }

    }
    

}