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
    public class FormsController : Controller
    {
        private readonly SpillTrackerDbContext _context;

        public FormsController(SpillTrackerDbContext context)
        {
            _context = context;
        }

        // GET: Forms
        public async Task<IActionResult> Index()
        {
            var spillTrackerDbContext = _context.Forms.Include(f => f.Chemical).Include(f => f.ChemicalState).Include(f => f.Facility).Include(f => f.SpillSurface).Include(f => f.Stuser);
            return View(await spillTrackerDbContext.ToListAsync());
        }

        // GET: Forms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _context.Forms
                .Include(f => f.Chemical)
                .Include(f => f.ChemicalState)
                .Include(f => f.Facility)
                .Include(f => f.SpillSurface)
                .Include(f => f.Stuser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (form == null)
            {
                return NotFound();
            }

            return View(form);
        }

        // GET: Forms/Create
        public IActionResult Create()
        {
            ViewData["ChemicalId"] = new SelectList(_context.Chemicals, "Id", "Id");
            ViewData["ChemicalStateId"] = new SelectList(_context.ChemicalStates, "Id", "Id");
            ViewData["FacilityId"] = new SelectList(_context.Facilities, "Id", "Id");
            ViewData["SpillSurfaceId"] = new SelectList(_context.Surfaces, "Id", "Id");
            ViewData["StuserId"] = new SelectList(_context.Stusers, "Id", "Id");
            return View();
        }

        // POST: Forms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SpillReportedBy,SpillReportedTime,SpillLocation,SpillOngoing,SpillContained,ChemicalPressurized,SpillVolume,SpillVolumeUnits,ChemicalConcentration,SpillFormingPuddle,SpillReachWaterSource,WaterSource,SpillDuration,CleanupStartTime,ChemicalTemperature,ChemicalTemperatureUnits,SpillWidth,SpillWidthUnits,SpillLength,SpillLengthUnits,SpillDepth,SpillDepthUnits,SpillArea,SpillAreaUnits,SpillReportable,WindDirection,WindSpeed,WindSpeedUnits,AddressStreet,AddressCity,AddressState,AddressZip,WeatherTemperature,WeatherTemperatureUnits,WeatherHumidity,WeatherHumidityUnits,SkyConditions,SpillEvaporationRate,SpillEvaporationRateUnits,AmountEvaporated,AmountEvaporatedUnits,AmountSpilled,AmountSpilledUnits,Notes,ContactNotes,StuserId,ChemicalId,SpillSurfaceId,ChemicalStateId,FacilityId")] Form form)
        {
            if (ModelState.IsValid)
            {
                _context.Add(form);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChemicalId"] = new SelectList(_context.Chemicals, "Id", "Id", form.ChemicalId);
            ViewData["ChemicalStateId"] = new SelectList(_context.ChemicalStates, "Id", "Id", form.ChemicalStateId);
            ViewData["FacilityId"] = new SelectList(_context.Facilities, "Id", "Id", form.FacilityId);
            ViewData["SpillSurfaceId"] = new SelectList(_context.Surfaces, "Id", "Id", form.SpillSurfaceId);
            ViewData["StuserId"] = new SelectList(_context.Stusers, "Id", "Id", form.StuserId);
            return View(form);
        }

        // GET: Forms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _context.Forms.FindAsync(id);
            if (form == null)
            {
                return NotFound();
            }
            ViewData["ChemicalId"] = new SelectList(_context.Chemicals, "Id", "Id", form.ChemicalId);
            ViewData["ChemicalStateId"] = new SelectList(_context.ChemicalStates, "Id", "Id", form.ChemicalStateId);
            ViewData["FacilityId"] = new SelectList(_context.Facilities, "Id", "Id", form.FacilityId);
            ViewData["SpillSurfaceId"] = new SelectList(_context.Surfaces, "Id", "Id", form.SpillSurfaceId);
            ViewData["StuserId"] = new SelectList(_context.Stusers, "Id", "Id", form.StuserId);
            return View(form);
        }

        // POST: Forms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SpillReportedBy,SpillReportedTime,SpillLocation,SpillOngoing,SpillContained,ChemicalPressurized,SpillVolume,SpillVolumeUnits,ChemicalConcentration,SpillFormingPuddle,SpillReachWaterSource,WaterSource,SpillDuration,CleanupStartTime,ChemicalTemperature,ChemicalTemperatureUnits,SpillWidth,SpillWidthUnits,SpillLength,SpillLengthUnits,SpillDepth,SpillDepthUnits,SpillArea,SpillAreaUnits,SpillReportable,WindDirection,WindSpeed,WindSpeedUnits,AddressStreet,AddressCity,AddressState,AddressZip,WeatherTemperature,WeatherTemperatureUnits,WeatherHumidity,WeatherHumidityUnits,SkyConditions,SpillEvaporationRate,SpillEvaporationRateUnits,AmountEvaporated,AmountEvaporatedUnits,AmountSpilled,AmountSpilledUnits,Notes,ContactNotes,StuserId,ChemicalId,SpillSurfaceId,ChemicalStateId,FacilityId")] Form form)
        {
            if (id != form.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(form);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormExists(form.Id))
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
            ViewData["ChemicalId"] = new SelectList(_context.Chemicals, "Id", "Id", form.ChemicalId);
            ViewData["ChemicalStateId"] = new SelectList(_context.ChemicalStates, "Id", "Id", form.ChemicalStateId);
            ViewData["FacilityId"] = new SelectList(_context.Facilities, "Id", "Id", form.FacilityId);
            ViewData["SpillSurfaceId"] = new SelectList(_context.Surfaces, "Id", "Id", form.SpillSurfaceId);
            ViewData["StuserId"] = new SelectList(_context.Stusers, "Id", "Id", form.StuserId);
            return View(form);
        }

        // GET: Forms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _context.Forms
                .Include(f => f.Chemical)
                .Include(f => f.ChemicalState)
                .Include(f => f.Facility)
                .Include(f => f.SpillSurface)
                .Include(f => f.Stuser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (form == null)
            {
                return NotFound();
            }

            return View(form);
        }

        // POST: Forms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var form = await _context.Forms.FindAsync(id);
            _context.Forms.Remove(form);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormExists(int id)
        {
            return _context.Forms.Any(e => e.Id == id);
        }
    }
}
