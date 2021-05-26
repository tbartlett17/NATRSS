using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SpillTracker.Models;
using SpillTracker.Utilities;
using SpillTracker.Models.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace SpillTracker.Controllers
{
    [Authorize(Roles = "Admin, FacilityManager, Employee")]
    public class FormsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SpillTrackerDbContext _context;
        private readonly IConfiguration _config;

        private readonly ISpillTrackerUserRepository _stuRepo;
        private readonly ISpillTrackerFormRepository _stfRepo;
        //private readonly ISpillTrackerChemicalRepository _stcRepo;



        public FormsController(SpillTrackerDbContext context, IConfiguration config, UserManager<IdentityUser> userManager, ISpillTrackerUserRepository stuRepo, ISpillTrackerFormRepository stfRepo)
        {
            _context = context;
            _config = config;
            _userManager = userManager;
            _stuRepo = stuRepo;
            _stfRepo = stfRepo;
            //_stcRepo = stcRepo;
        }

        // GET: Forms
        public async Task<IActionResult> Index()
        {
            // Information from Identity through the User Manager
            string userId = _userManager.GetUserId(User);
            IdentityUser identityUser = await _userManager.GetUserAsync(User);

            Stuser stUser = null;
            if (userId != null)
            {
                stUser = _stuRepo.GetStuserByIdentityId(userId);
                //stUser = _context.Stusers.Where(stu => stu.AspnetIdentityId == userId).FirstOrDefault();
            }

            //var spillTrackerDbContext = _context.Forms.Include(f => f.Chemical).Include(f => f.ChemicalState).Include(f => f.Facility).Include(f => f.SpillSurface).Include(f => f.Stuser).Take(0);
            IEnumerable<Form> formsList = null;

            if (User.IsInRole("Admin")) 
            {
                //spillTrackerDbContext = _context.Forms.Include(f => f.Chemical).Include(f => f.ChemicalState).Include(f => f.Facility).Include(f => f.SpillSurface).Include(f => f.Stuser);
                formsList = _stfRepo.GetAll();
            }
            if (User.IsInRole("FacilityManager") || User.IsInRole("Employee")) 
            {

                //var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                //var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                //string userId = claim.Value;

                //// look up the current user in the spill tracker DB
                //Stuser currentUser = _context.Stusers.Where(stu => stu.AspnetIdentityId == userId).FirstOrDefault();


                if (stUser.CompanyId != null)
                {
                    formsList = _stfRepo.GetAllFormsByCompanyId((int)stUser.CompanyId);
                }


               
                //spillTrackerDbContext = _context.Forms.Include(f => f.Chemical)
                //            .Include(f => f.ChemicalState)
                //            .Include(f => f.Facility).ThenInclude(f => f.Company)
                //            .Include(f => f.SpillSurface)
                //            .Include(f => f.Stuser).Where(c => c.Facility.CompanyId == stUser.CompanyId);

                ViewData["FacilityList"] = new SelectList(_context.Facilities.Where(x => x.CompanyId == stUser.CompanyId).OrderBy(x => x.Name), "Id", "Name");
            }

            return View(formsList);
            //return View(await spillTrackerDbContext.ToListAsync());
        }

        // GET: Forms/Details/5
        public async Task<IActionResult> Details(int? id)
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

            var form = await _context.Forms
                .Include(f => f.FacilityChemical)
                .ThenInclude(fc => fc.Chemical)
                .Include(f => f.FacilityChemical)
                .ThenInclude(fc => fc.ChemicalState)
                .Include(f => f.ChemicalState)
                .Include(f => f.Facility)
                .Include(f => f.SpillSurface)
                .Include(f => f.Stuser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (form == null)
            {
                return NotFound();
            }
            if (User.IsInRole("Admin"))
            {
                return View(form);
            }
            else if(currentUser.CompanyId == form.Facility.CompanyId) 
            {
                return View(form);
            }
            else
            {
                return Redirect("~/Identity/Account/AccessDenied"); // need to return access denied because user tried accessing company data they are not apart of 
            }

        }

        // GET: Forms/Create
        [Authorize(Roles = "Admin, FacilityManager, Employee")]
        public IActionResult Create()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            string userId = claim.Value;
            Stuser currentUser = _context.Stusers.Where(stu => stu.AspnetIdentityId == userId).FirstOrDefault();
            

            Form form = new Form();
            form.SpillContained = false;
            form.SpillOngoing = false;
            form.SpillReachWaterSource = false;
            form.SpillReportable = false;
            form.WaterSource = "";
            //form.Facility = _context.Facilities.Where(f => f.Id == id).FirstOrDefault();

            ViewData["ChemicalId"] = new SelectList(_context.FacilityChemicals
                .Include(fc => fc.Chemical)
                .Where(fc=> fc.Facility.CompanyId == currentUser.CompanyId)
                .OrderBy(fc => fc.Chemical.Name)
                ,"Id", "Chemical.Name");
            ViewData["ChemicalStateId"] = new SelectList(_context.ChemicalStates.OrderBy(x => x.Type), "Id", "Type");


            // all facilites at the company
            //ViewData["FacilityId"] = new SelectList(_context.Facilities.Where(f => f.CompanyId == currentUser.CompanyId), "Id", "Name"); 

            // select all facilities the current user has access to
            IQueryable<StuserFacility> stusersFacilities = _context.StuserFacilities.Where(uf => uf.StuserId == currentUser.Id);
            List<int?> idList = new List<int?>();
            foreach (var item in stusersFacilities)
            {
                idList.Add(item.FacilityId);
            }
            IQueryable<Facility> usersFacilitesList = _context.Facilities.Include(f => f.Company).Where(f => idList.Contains(f.Id));
            ViewData["FacilityId"] = new SelectList(usersFacilitesList, "Id", "Name");


            ViewData["SpillSurfaceId"] = new SelectList(_context.Surfaces.OrderBy(x => x.Type), "Id", "Type");
            ViewData["StuserId"] = new SelectList(_context.Stusers.OrderBy(x => x.FirstName), "Id", "FirstName");
            return View(form);
        }

        // POST: Forms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, FacilityManager, Employee")]
        public async Task<IActionResult> Create([Bind("Id,SpillReportedBy,SpillReportedDate,SpillReportedTime,SpillLocation,SpillOngoing,SpillContained,NeedAssistance,ChemicalPressurized,SpillVolume,SpillVolumeUnits,ChemicalConcentration,SpillFormingPuddle,SpillReachWaterSource,WaterSource,SpillDuration,CleanupStartTime,ChemicalTemperature,ChemicalTemperatureUnits,SpillWidth,SpillWidthUnits,SpillLength,SpillLengthUnits,SpillDepth,SpillDepthUnits,SpillArea,SpillAreaUnits,SpillReportable,WindDirection,WindSpeed,WindSpeedUnits,AddressStreet,AddressCity,AddressState,AddressZip,WeatherTemperature,WeatherTemperatureUnits,WeatherHumidity,WeatherHumidityUnits,SkyConditions,SpillEvaporationRate,SpillEvaporationRateUnits,AmountEvaporated,AmountEvaporatedUnits,AmountSpilled,AmountSpilledUnits,Notes,ContactNotes,StuserId,FacilityChemicalId,SpillSurfaceId,ChemicalStateId,FacilityId")] Form form)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            string userId = claim.Value;
            Stuser currentUser = _context.Stusers.Where(stu => stu.AspnetIdentityId == userId).FirstOrDefault();
            form.StuserId = currentUser.Id;

            Debug.WriteLine(form.Id);
            form.Id = 0; // resets form id to it save to db correctly

            if(!String.IsNullOrEmpty(form.WaterSource)) 
            {
                form.SpillReachWaterSource = true;
            }
            form.SpillArea = (form.SpillLength * form.SpillWidth);
            form.SpillAreaUnits = (form.SpillLengthUnits + "\u00B2");
            
            if (ModelState.IsValid)
            {
                _context.Add(form);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChemicalId"] = new SelectList(_context.FacilityChemicals
                .Include(fc => fc.Chemical)
                .Where(fc => fc.Facility.CompanyId == currentUser.CompanyId)
                .OrderBy(fc => fc.Chemical.Name)
                , "Id", "Chemical.Name");
            ViewData["ChemicalStateId"] = new SelectList(_context.ChemicalStates.OrderBy(x => x.Type), "Id", "Type");
            ViewData["FacilityId"] = new SelectList(_context.Facilities.Where(f => f.CompanyId == currentUser.CompanyId), "Id", "Name");
            ViewData["SpillSurfaceId"] = new SelectList(_context.Surfaces.OrderBy(x => x.Type), "Id", "Type");
            ViewData["StuserId"] = new SelectList(_context.Stusers.OrderBy(x => x.FirstName), "Id", "FirstName");
            return View(form);
        }

        // GET: Forms/Edit/5
        [Authorize(Roles = "Admin, FacilityManager")]
        public async Task<IActionResult> Edit(int? id)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            string userId = claim.Value;

            // look up the current user in the spill tracker DB
            Stuser currentUser = _context.Stusers.Where(stu => stu.AspnetIdentityId == userId).FirstOrDefault();

            if (id == null)
            {
                return NotFound(); 
            }

            var form =  _context.Forms.Include(c => c.Facility).ThenInclude(x => x.Company).Where(i => i.Id == id).FirstOrDefault();

            if (form == null)
            {
                return NotFound();
            }
            ViewData["ChemicalId"] = new SelectList(_context.FacilityChemicals.Include(fc => fc.Chemical).Where(fc=> fc.FacilityId == id).OrderBy(fc => fc.Chemical.Name), "ChemicalId", "Chemical.Name");
            ViewData["ChemicalStateId"] = new SelectList(_context.ChemicalStates, "Id", "Type", form.ChemicalStateId);
            ViewData["FacilityId"] = new SelectList(_context.Facilities, "Id", "Name", form.FacilityId);
            ViewData["SpillSurfaceId"] = new SelectList(_context.Surfaces, "Id", "Type", form.SpillSurfaceId);
            ViewData["StuserId"] = new SelectList(_context.Stusers, "Id", "FirstName", form.StuserId);
            //Debug.WriteLine(form.Facility.CompanyId);
            if (User.IsInRole("Admin"))
            {
                return View(form);
            }
            else if(currentUser.CompanyId == form.Facility.CompanyId)
            {
                return View(form);
            }
            else
            {
                return Redirect("~/Identity/Account/AccessDenied"); // need to return access denied because user tried accessing a company data they are not apart of 
            }      
        }

        // POST: Forms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, FacilityManager")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SpillReportedBy,SpillReportedDate,SpillReportedTime,SpillContained,SpillOngoing,NeedAssistance,SpillLocation,ChemicalPressurized,SpillVolume,SpillVolumeUnits,ChemicalConcentration,SpillFormingPuddle,SpillReachWaterSource,WaterSource,SpillDurationHours,SpillDurationMinutes,CleanupStartTime,ChemicalTemperature,ChemicalTemperatureUnits,SpillWidth,SpillWidthUnits,SpillLength,SpillLengthUnits,SpillDepth,SpillDepthUnits,SpillArea,SpillAreaUnits,SpillReportable,WindDirection,WindSpeed,WindSpeedUnits,AddressStreet,AddressCity,AddressState,AddressZip,WeatherTemperature,WeatherTemperatureUnits,WeatherHumidity,WeatherHumidityUnits,SkyConditions,SpillEvaporationRate,SpillEvaporationRateUnits,AmountEvaporated,AmountEvaporatedUnits,AmountSpilled,AmountSpilledUnits,Notes,ContactNotes,StuserId,FacilityChemicalId,SpillSurfaceId,ChemicalStateId,FacilityId")] Form form)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            string userId = claim.Value;
             Stuser currentUser = _context.Stusers.Where(stu => stu.AspnetIdentityId == userId).FirstOrDefault();

            //form.StuserId = currentUser.Id;



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
            }ViewData["ChemicalId"] = new SelectList(_context.FacilityChemicals.Include(fc => fc.Chemical).Where(fc=> fc.FacilityId == id).OrderBy(fc => fc.Chemical.Name), "ChemicalId", "Chemical.Name");
            //ViewData["ChemicalId"] = new SelectList(_context.Chemicals.OrderBy(x => x.Name), "Id", "Name");
            ViewData["ChemicalStateId"] = new SelectList(_context.ChemicalStates.OrderBy(x => x.Type), "Id", "Type");
            ViewData["FacilityId"] = new SelectList(_context.Facilities.Where(x => x.CompanyId == currentUser.CompanyId).OrderBy(x => x.Name), "Id", "Name");
            ViewData["SpillSurfaceId"] = new SelectList(_context.Surfaces.OrderBy(x => x.Type), "Id", "Type");
            ViewData["StuserId"] = new SelectList(_context.Stusers.OrderBy(x => x.FirstName), "Id", "FirstName");
            return View(form);
        }

        public IActionResult GetWeatherReport(string coords, DateTime dateTime)
        {
            WeatherReport wr = new WeatherReport();
            try
            {
                string apiKey = _config["OpenWeatherMapsAPIKey"];
                //Debug.WriteLine($"coords: {coords}, dateTime: {dateTime}");
                wr = GetWeatherInfo.GetWeather(coords, dateTime, apiKey);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Response.StatusCode = 500;
            }
            return Json(wr);
        }

        public IActionResult GetChemicalData(int id)
        {
            FacilityChemical fc = new FacilityChemical();
            try
            {
                fc = _context.FacilityChemicals
                    .Include(fc => fc.Chemical)
                    .Include(fc => fc.ChemicalState)
                    .Where(fc => fc.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Response.StatusCode = 500;
            }
            return Json(fc);
        }

        public IActionResult GetFacilityData(int id)
        {
            Facility fac = new Facility();
            try
            {
                fac = _context.Facilities
                    .Include(fc => fc.FacilityChemicals)
                    .ThenInclude(fc => fc.ChemicalState)
                    .Include(fc => fc.FacilityChemicals)
                    .ThenInclude(fc => fc.Chemical)
                    .Where(fc => fc.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Response.StatusCode = 500;
            }
            return Json(fac);
        }




        // GET: Forms/Delete/5
        //     public async Task<IActionResult> Delete(int? id)
        //     {
        //         if (id == null)
        //         {
        //             return NotFound();
        //         }

        //         var form = await _context.Forms
        //             .Include(f => f.Chemical)
        //             .Include(f => f.ChemicalState)
        //             .Include(f => f.Facility)
        //             .Include(f => f.SpillSurface)
        //             .Include(f => f.Stuser)
        //             .FirstOrDefaultAsync(m => m.Id == id);
        //         if (form == null)
        //         {
        //             return NotFound();
        //         }

        //         return View(form);
        //     }

        //     // POST: Forms/Delete/5
        //     [HttpPost, ActionName("Delete")]
        //     [ValidateAntiForgeryToken]
        //     public async Task<IActionResult> DeleteConfirmed(int id)
        //     {
        //         var form = await _context.Forms.FindAsync(id);
        //         _context.Forms.Remove(form);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }

        private bool FormExists(int id)
        {
            return _context.Forms.Any(e => e.Id == id);
        }        
    }
}
