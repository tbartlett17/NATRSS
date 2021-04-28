using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpillTracker.Models;
using Microsoft.AspNetCore.Authorization;


namespace SpillTracker.Controllers
{
    [AllowAnonymous]
    public class CalculationController : Controller
    {
        private readonly SpillTrackerDbContext db;

        public CalculationController(SpillTrackerDbContext context)
        {
            
            db = context;
        }

        [HttpGet]
        public IActionResult Index(Calculation calc) 
        {
            if(ModelState.IsValid) 
            {
                calc.chemicals = db.Chemicals.Where(c => c.Name.Contains(calc.input) || c.CasNum.Contains(calc.input)).ToList();
                return View("Index", calc);
            }
            else 
            {
                return View("Index", null);
            } 
        }

        
        public IActionResult Output(int id) 
        {
            Calculation calc = new Calculation();

            calc.chemID = id;
            Chemical chem = db.Chemicals.Where(c => c.Id == id).FirstOrDefault();
            if(chem == null)
            {
                return NotFound();
            }
            calc.chemName = chem.Name;
            calc.reportableQuantity = chem.ReportableQuantity;
            calc.chemNum = chem.CasNum;
            calc.cercla = chem.CerclaChem;


            return View("Output", calc);
        }

    }
}