using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpillTracker.Models;

namespace SpillTracker.Controllers
{
    public class ChemicalsController : Controller
    {
        private readonly SpillTrackerDbContext _context;

        public ChemicalsController(SpillTrackerDbContext context)
        {
            _context = context;
        }

        // GET: Chemicals
        public async Task<IActionResult> Index()
        {
            /*return View(await _context.Chemicals.ToListAsync());*/
            return View(await _context.Chemicals.OrderBy(x=>x.Name).ToListAsync());        
        }

        // GET: Chemicals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chemical = await _context.Chemicals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chemical == null)
            {
                return NotFound();
            }

            return View(chemical);
        }
        
        // public IActionResult Search(string searchString) {
        //     if(ModelState.IsValid) {
        //         return View(_context.Chemicals.Where(s => s.Name.Contains(searchString)).ToListAsync());
        //     }
        //     else {
        //         return View("Index", null);
        //     } 
        // }


        // GET: Chemicals/Create
        // public IActionResult Create()
        // {
        //     return View();
        // }

        // // POST: Chemicals/Create
        // // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create([Bind("Id,Name,CasNum,PubChemCid,ReportableQuantity,ReportableQuantityUnits,Density,DensityUnits,MolecularWeight,MolecularWeightUnits,VaporPressure,VaporPressureUnits,CerclaChem,EpcraChem")] Chemical chemical)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         _context.Add(chemical);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(chemical);
        // }

        // GET: Chemicals/Edit/5
        // public async Task<IActionResult> Edit(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var chemical = await _context.Chemicals.FindAsync(id);
        //     if (chemical == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(chemical);
        // }

        // POST: Chemicals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CasNum,PubChemCid,ReportableQuantity,ReportableQuantityUnits,Density,DensityUnits,MolecularWeight,MolecularWeightUnits,VaporPressure,VaporPressureUnits,CerclaChem,EpcraChem")] Chemical chemical)
        // {
        //     if (id != chemical.Id)
        //     {
        //         return NotFound();
        //     }

        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             _context.Update(chemical);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!ChemicalExists(chemical.Id))
        //             {
        //                 return NotFound();
        //             }
        //             else
        //             {
        //                 throw;
        //             }
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(chemical);
        // }

        // GET: Chemicals/Delete/5
        // public async Task<IActionResult> Delete(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var chemical = await _context.Chemicals
        //         .FirstOrDefaultAsync(m => m.Id == id);
        //     if (chemical == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(chemical);
        // }

        // // POST: Chemicals/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed(int id)
        // {
        //     var chemical = await _context.Chemicals.FindAsync(id);
        //     _context.Chemicals.Remove(chemical);
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        // }

        private bool ChemicalExists(int id)
        {
            return _context.Chemicals.Any(e => e.Id == id);
        }
    }
}
