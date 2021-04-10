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
    public class ExpeditionsController : Controller
    {
        private readonly HimalayasDbContext _context;

        public ExpeditionsController(HimalayasDbContext context)
        {
            _context = context;
        }

        // GET: Expeditions
        public async Task<IActionResult> Index()
        {
            ViewData["PeakId"] = new SelectList(_context.Peaks, "Id", "Name");
            var expeditionsDbContext = _context.Expeditions.OrderByDescending(x => x).Include(e => e.Peak).Include(e => e.TrekkingAgency).Take(50);
            expeditionsDbContext.Reverse();
            var sort = new PeakSort()
            {
                expeditions = await expeditionsDbContext.ToListAsync(),
                peaks = await _context.Peaks.ToListAsync(),
                
            };
            
            return View(sort);
        }

        // GET: Expeditions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expedition = await _context.Expeditions
                .Include(e => e.Peak)
                .Include(e => e.TrekkingAgency)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expedition == null)
            {
                return NotFound();
            }

            return View(expedition);
        }

        public IActionResult SortbyDate()
        {
            ViewData["PeakId"] = new SelectList(_context.Peaks, "Id", "Name");
            var trek = _context.Expeditions
            .OrderByDescending(d => d.Season)
            .Include(p => p.Peak)
            .Include(t => t.TrekkingAgency).ToList();

            trek.Reverse();

            var sort = new PeakSort()
            {
                expeditions = trek.Take(50),
                peaks =  _context.Peaks.ToList()
            };
            return View("Index", sort);
        }

        public IActionResult SelectPeak(PeakSort mtn)
        {
            ViewData["PeakId"] = new SelectList(_context.Peaks, "Id", "Name");

            var trek = _context.Expeditions
                .OrderByDescending(d => d.StartDate)
                .Include(p => p.Peak)
                .Include(t => t.TrekkingAgency)
                .Where(p => p.Peak.Id == mtn.mtnPeak.Id).ToList();

            var sort = new PeakSort()
            {
                expeditions = trek.Take(50),
                peaks = _context.Peaks.ToList()
            };
            return View("Index", sort);
        }

        public IActionResult SortbyPeak()
        {
            ViewData["PeakId"] = new SelectList(_context.Peaks, "Id", "Name");
            var trek = _context.Expeditions
            .OrderByDescending(d => d.Peak)
            .Include(p => p.Peak)
            .Include(t => t.TrekkingAgency).ToList();

            trek.Reverse();

            var sort = new PeakSort()
            {
                expeditions = trek.Take(50),
                peaks = _context.Peaks.ToList()
            };
            return View("Index", sort);
        }

        public IActionResult SortbySuccess()
        {
            
            ViewData["PeakId"] = new SelectList(_context.Peaks, "Id", "Name");
            var trek = _context.Expeditions
            .Where(s => s.TerminationReason.Contains("cess"))
            .Include(p => p.Peak)
            .Include(t => t.TrekkingAgency).ToList();

            trek.Reverse();

            var sort = new PeakSort()
            {
                expeditions = trek.Take(50),
                peaks = _context.Peaks.ToList()
            };
            return View("Index", sort);
        }

       
        // GET: Expeditions/Create
        public IActionResult Create()
        {
            ViewData["PeakId"] = new SelectList(_context.Peaks.OrderBy(x => x.Name), "Id", "Name");
            ViewData["TrekkingAgencyId"] = new SelectList(_context.TrekkingAgencies.OrderBy(x => x.Name), "Id", "Name");
            
            return View();
        }

        // POST: Expeditions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Season,Year,StartDate,TerminationReason,OxygenUsed,PeakId,TrekkingAgencyId")] Expedition expedition)
        {
            
            

            if (ModelState.IsValid)
            {
                _context.Add(expedition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PeakId"] = new SelectList(_context.Peaks, "Id", "Name", expedition.PeakId);
            ViewData["TrekkingAgencyId"] = new SelectList(_context.TrekkingAgencies, "Id", "Id", expedition.TrekkingAgencyId);
            return View(expedition);
        }

        // GET: Expeditions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expedition = await _context.Expeditions.FindAsync(id);
            if (expedition == null)
            {
                return NotFound();
            }
            ViewData["PeakId"] = new SelectList(_context.Peaks, "Id", "Name", expedition.PeakId);
            ViewData["TrekkingAgencyId"] = new SelectList(_context.TrekkingAgencies, "Id", "Id", expedition.TrekkingAgencyId);
            return View(expedition);
        }

        // POST: Expeditions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Season,Year,StartDate,TerminationReason,OxygenUsed,PeakId,TrekkingAgencyId")] Expedition expedition)
        {
            if (id != expedition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expedition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpeditionExists(expedition.Id))
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
            ViewData["PeakId"] = new SelectList(_context.Peaks, "Id", "Name", expedition.PeakId);
            ViewData["TrekkingAgencyId"] = new SelectList(_context.TrekkingAgencies, "Id", "Id", expedition.TrekkingAgencyId);
            return View(expedition);
        }

        // GET: Expeditions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expedition = await _context.Expeditions
                .Include(e => e.Peak)
                .Include(e => e.TrekkingAgency)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expedition == null)
            {
                return NotFound();
            }

            return View(expedition);
        }

        // POST: Expeditions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expedition = await _context.Expeditions.FindAsync(id);
            _context.Expeditions.Remove(expedition);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpeditionExists(int id)
        {
            return _context.Expeditions.Any(e => e.Id == id);
        }
    }
}
