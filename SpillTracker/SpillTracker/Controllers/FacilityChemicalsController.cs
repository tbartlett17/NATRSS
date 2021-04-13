using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpillTracker.Models;

namespace SpillTracker
{
    public class FacilityChemicalsController : Controller
    {
        private readonly SpillTrackerDbContext _context;

        public FacilityChemicalsController(SpillTrackerDbContext context)
        {
            _context = context;
        }

        // GET: FacilityChemicals
        public async Task<IActionResult> Index()
        {
            var spillTrackerDbContext = _context.FacilityChemicals.Include(f => f.Chemical).Include(f => f.ChemicalState).Include(f => f.Facility);
            return View(await spillTrackerDbContext.ToListAsync());
        }

        // GET: FacilityChemicals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilityChemical = await _context.FacilityChemicals
                .Include(f => f.Chemical)
                .Include(f => f.ChemicalState)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facilityChemical == null)
            {
                return NotFound();
            }

            return View(facilityChemical);
        }

        // GET: FacilityChemicals/Create
        public IActionResult Create()
        {
            ViewData["ChemicalId"] = new SelectList(_context.Chemicals, "Id", "Id");
            ViewData["ChemicalStateId"] = new SelectList(_context.ChemicalStates, "Id", "Id");
            ViewData["FacilityId"] = new SelectList(_context.Facilities, "Id", "Id");
            return View();
        }

        // POST: FacilityChemicals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Concentration,ChemicalTemperature,ChemicalTemperatureUnits,ChemicalStateId,ChemicalId,FacilityId")] FacilityChemical facilityChemical)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facilityChemical);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChemicalId"] = new SelectList(_context.Chemicals, "Id", "Id", facilityChemical.ChemicalId);
            ViewData["ChemicalStateId"] = new SelectList(_context.ChemicalStates, "Id", "Id", facilityChemical.ChemicalStateId);
            ViewData["FacilityId"] = new SelectList(_context.Facilities, "Id", "Id", facilityChemical.FacilityId);
            return View(facilityChemical);
        }

        // GET: FacilityChemicals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilityChemical = await _context.FacilityChemicals.FindAsync(id);
            if (facilityChemical == null)
            {
                return NotFound();
            }
            ViewData["ChemicalId"] = new SelectList(_context.Chemicals, "Id", "Id", facilityChemical.ChemicalId);
            ViewData["ChemicalStateId"] = new SelectList(_context.ChemicalStates, "Id", "Id", facilityChemical.ChemicalStateId);
            ViewData["FacilityId"] = new SelectList(_context.Facilities, "Id", "Id", facilityChemical.FacilityId);
            return View(facilityChemical);
        }

        // POST: FacilityChemicals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Concentration,ChemicalTemperature,ChemicalTemperatureUnits,ChemicalStateId,ChemicalId,FacilityId")] FacilityChemical facilityChemical)
        {
            if (id != facilityChemical.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facilityChemical);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacilityChemicalExists(facilityChemical.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChemicalId"] = new SelectList(_context.Chemicals, "Id", "Id", facilityChemical.ChemicalId);
            ViewData["ChemicalStateId"] = new SelectList(_context.ChemicalStates, "Id", "Id", facilityChemical.ChemicalStateId);
            ViewData["FacilityId"] = new SelectList(_context.Facilities, "Id", "Id", facilityChemical.FacilityId);
            return View(facilityChemical);
        }

        // GET: FacilityChemicals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilityChemical = await _context.FacilityChemicals
                .Include(f => f.Chemical)
                .Include(f => f.ChemicalState)
                .Include(f => f.Facility)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facilityChemical == null)
            {
                return NotFound();
            }

            return View(facilityChemical);
        }

        // POST: FacilityChemicals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facilityChemical = await _context.FacilityChemicals.FindAsync(id);
            _context.FacilityChemicals.Remove(facilityChemical);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacilityChemicalExists(int id)
        {
            return _context.FacilityChemicals.Any(e => e.Id == id);
        }
    }
}
