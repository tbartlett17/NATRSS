using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpillTracker.Models;

namespace SpillTracker.Controllers
{
    [Authorize(Roles = "Admin, FacilityManager, Employee")]
    public class FacilitiesController : Controller
    {
        private readonly SpillTrackerDbContext _context;

        public FacilitiesController(SpillTrackerDbContext context)
        {
            _context = context;
        }

        // GET: Facilities
        public async Task<IActionResult> Index()
        {
            var spillTrackerDbContext = _context.Facilities.Include(f => f.Company).Take(0); // default show no facilities

            if (User.IsInRole("Admin")) // shows the admin all facilities
            {
                spillTrackerDbContext = _context.Facilities.Include(f => f.Company); // shows the admin all facilities
            }
            if (User.IsInRole("FacilityManager") || User.IsInRole("Employee")) // shows the company employees their facilities
            {
                // get the current users identity ID
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                string userId = claim.Value;

                // look up the current user in the spill tracker DB
                Stuser currentUser = _context.Stusers.Where(stu => stu.AspnetIdentityId == userId).FirstOrDefault();
                //Debug.WriteLine("\n\n STuser aspnet identity id: " + currentUser.AspnetIdentityId);

                // select this user's company's facilities
                spillTrackerDbContext = _context.Facilities  .Include(f => f.Company);
            }

            //var spillTrackerDbContext = _context.Facilities.Include(f => f.Company);
            return View(await spillTrackerDbContext.ToListAsync());
        }

        // GET: Facilities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // get the current users identity ID
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            string userId = claim.Value;

            // look up the current user in the spill tracker DB
            Stuser currentUser = _context.Stusers.Where(stu => stu.AspnetIdentityId == userId).FirstOrDefault();


            
            FacilityManagementVM facilityManagementVM = new FacilityManagementVM();

            if (id == null)
            {
                return NotFound();
            }

            facilityManagementVM.Facility = await _context.Facilities
                .Include(f => f.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facilityManagementVM == null)
            {
                return NotFound();
            }

            facilityManagementVM.FacilityChemicals = _context.FacilityChemicals
                .Include(fc => fc.Chemical)
                .Include(fc => fc.ChemicalState)
                .Where(f => f.FacilityId == id);

            if (currentUser.CompanyId == facilityManagementVM.Facility.CompanyId || User.IsInRole("Admin"))
            {
                return View(facilityManagementVM);
            }
            else
            {
                return Redirect("~/Identity/Account/AccessDenied"); // need to return access denied because user tried accessing company data they are not apart of 
            }
        }

        // GET: Facilities/Create
        [Authorize(Roles = "Admin, FacilityManager")]
        public IActionResult Create()

        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id");
            return View();
        }

        // POST: Facilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, FacilityManager")]
        public async Task<IActionResult> Create([Bind("Id,Name,AddressStreet,AddressCity,AddressState,AddressZip,Location,Industry,CompanyId")] Facility facility)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facility);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", facility.CompanyId);
            return View(facility);
        }

        // GET: Facilities/Edit/5
        [Authorize(Roles = "Admin, FacilityManager")]
        public async Task<IActionResult> Edit(int? id)
        {
            // get the current users identity ID
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            string userId = claim.Value;

            // look up the current user in the spill tracker DB
            Stuser currentUser = _context.Stusers.Where(stu => stu.AspnetIdentityId == userId).FirstOrDefault();

            if (id == null)
            {
                return NotFound();
            }

            var facility = await _context.Facilities.FindAsync(id);
            if (facility == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", facility.CompanyId);

            if (currentUser.CompanyId == facility.CompanyId || User.IsInRole("Admin"))
            {
                return View(facility);
            }
            else
            {
                return Redirect("~/Identity/Account/AccessDenied"); // need to return access denied because user tried accessing a company data they are not apart of 
            }
        }

        // POST: Facilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,AddressStreet,AddressCity,AddressState,AddressZip,Location,Industry,CompanyId")] Facility facility)
        {
            if (id != facility.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facility);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacilityExists(facility.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", facility.CompanyId);
            return View(facility);
        }

        // GET: Facilities/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facility = await _context.Facilities
                .Include(f => f.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facility == null)
            {
                return NotFound();
            }

            return View(facility);
        }

        // POST: Facilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facility = await _context.Facilities.FindAsync(id);
            _context.Facilities.Remove(facility);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacilityExists(int id)
        {
            return _context.Facilities.Any(e => e.Id == id);
        }

        // GET: Facilities/Edit/5
        [Authorize(Roles = "Admin, FacilityManager")]
        public async Task<IActionResult> EditFacChems(int? id)
        {
            // get the current users identity ID
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            string userId = claim.Value;

            // look up the current user in the spill tracker DB
            Stuser currentUser = _context.Stusers.Where(stu => stu.AspnetIdentityId == userId).FirstOrDefault();

            if (id == null)
            {
                return NotFound();
            }

            var facility = await _context.Facilities.FindAsync(id);
            if (facility == null)
            {
                return NotFound();
            }

            EditFacilityChemsVM chems = new EditFacilityChemsVM();

            chems.Chemicals = await _context.Chemicals.OrderBy(x => x.Name).ToListAsync();
            chems.FacilityChemicals = _context.FacilityChemicals
                .Include(fc => fc.Chemical)
                .Include(fc => fc.ChemicalState)
                .Where(f => f.FacilityId == id);


            if (currentUser.CompanyId == facility.CompanyId || User.IsInRole("Admin"))
            {
                return View(chems);
            }
            else
            {
                return Redirect("~/Identity/Account/AccessDenied"); // need to return access denied because user tried accessing a company data they are not apart of 
            }
        }

        //// POST: Facilities/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditChems(int id, [Bind("Id,Name,AddressStreet,AddressCity,AddressState,AddressZip,Location,Industry,CompanyId")] Facility facility)
        //{
        //    if (id != facility.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(facility);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!FacilityExists(facility.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", facility.CompanyId);
        //    return View(facility);
        //}
    }
}
