using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpeditionProject.Models;

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
    }
    

}