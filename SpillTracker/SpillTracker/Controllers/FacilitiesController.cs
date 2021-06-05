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
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SpillTracker.Models;
using SpillTracker.Models.Interfaces;

namespace SpillTracker.Controllers
{
    [Authorize(Roles = "Admin, FacilityManager, Employee")]
    public class FacilitiesController : Controller
    {
        private readonly SpillTrackerDbContext _context;
        private readonly ILogger<FacilitiesController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly ISpillTrackerUserRepository _stUserRepo;
        private readonly ISpillTrackerFacilityRepository _stFacRepo;
        private readonly ISpillTrackerFacilityChemicalRepository _stFacChemRepo;
        private readonly ISpillTrackerStuserFacilityRepository _stUserFacRepo;
        private readonly ISpillTrackerCompanyRepository _stCompRepo;

        public FacilitiesController(SpillTrackerDbContext context, ILogger<FacilitiesController> logger, UserManager<IdentityUser> userManager, 
            ISpillTrackerUserRepository stUserRepo, ISpillTrackerFacilityRepository stFacRepo, ISpillTrackerFacilityChemicalRepository stFacChemRepo,
            ISpillTrackerStuserFacilityRepository stUserFacRepo, ISpillTrackerCompanyRepository stCompRepo)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _stUserRepo = stUserRepo;
            _stFacRepo = stFacRepo;
            _stFacChemRepo = stFacChemRepo;
            _stUserFacRepo = stUserFacRepo;
            _stCompRepo = stCompRepo;
        }

        // GET: Facilities
        public async Task<IActionResult> Index()
        {
            FacilityManagementVM newFac = new FacilityManagementVM();
            var spillTrackerDbContext = _context.Facilities.Include(f => f.Company).Take(0); // default show no facilities
                // get the current users identity ID
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                string userId = claim.Value;

                // look up the current user in the spill tracker DB
                Stuser currentUser = _context.Stusers.Include(u => u.Company).Where(stu => stu.AspnetIdentityId == userId).FirstOrDefault();
            if (User.IsInRole("Admin")) // shows the admin all facilities
            {
                spillTrackerDbContext = _context.Facilities.Include(f => f.Company); // shows the admin all facilities
                newFac.Facility = spillTrackerDbContext.ToList();
                newFac.User = currentUser;
            }
            if (User.IsInRole("FacilityManager") || User.IsInRole("Employee")) // shows the company employees their facilities
            {

                //Debug.WriteLine("\n\n STuser aspnet identity id: " + currentUser.AspnetIdentityId);

                // select this user's company's facilities
                //spillTrackerDbContext = _context.Facilities.Where(f => f.CompanyId == currentUser.CompanyId).Include(f => f.Company);
                //newFac.Facility = spillTrackerDbContext.ToList();
                //newFac.user = currentUser;

                // select all facilities the current user has access to
                var usersFacilities = _context.StuserFacilities.Where(uf => uf.StuserId == currentUser.Id);
                List<int?> idList = new List<int?>();
                foreach(var item in usersFacilities)
                {
                    idList.Add(item.FacilityId);
                }

                spillTrackerDbContext = _context.Facilities.Include(f => f.Company).Where(f => idList.Contains(f.Id));
                newFac.Facility = spillTrackerDbContext.ToList();
                newFac.User = currentUser;
            }

            //var spillTrackerDbContext = _context.Facilities.Include(f => f.Company);
            return View(newFac);
        }


        public IActionResult SetCompany(FacilityManagementVM newFac)
        {
            var code = newFac.Codes;
            if (code == null)
            {
                return NotFound();
            }
            else
            {    
                _logger.LogInformation(code);
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                string userId = claim.Value;

                // look up the current user in the spill tracker DB
                Stuser currentUser = _context.Stusers.Where(stu => stu.AspnetIdentityId == userId).FirstOrDefault();
                code = code.ToUpper();
                
                Company findCompany = new Company();
                findCompany = _context.Companies.Where(x => x.AccessCode.ToUpper().Equals(code)).FirstOrDefault();
                

                if(currentUser.CompanyId == null && findCompany != null)
                {
                    currentUser.CompanyId = findCompany.Id;
                    // newFac.user.CompanyId = findCompany.Id;
                    _context.Stusers.Update(currentUser);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                    return RedirectToAction("Index");
            } 
        }

        public IActionResult JoinFacility(FacilityManagementVM facilityManagementVM)
        {
            var code = facilityManagementVM.Codes;
            if (code == null)
            {
                return NotFound();
            }
            else
            {           
                _logger.LogInformation(code);
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                string userId = claim.Value;

                // look up the current user in the spill tracker DB
                Stuser currentUser = _context.Stusers.Where(stu => stu.AspnetIdentityId == userId).FirstOrDefault();
                code = code.ToUpper();
                
                Facility findFacility = new Facility();
                findFacility = _context.Facilities.Where(x => x.AccessCode.ToUpper().Equals(code)).FirstOrDefault();
                

                if(findFacility != null && currentUser.CompanyId == findFacility.CompanyId)
                {
                    StuserFacility newUserFac = new StuserFacility
                    {
                        FacilityId = findFacility.Id,
                        StuserId = currentUser.Id                    
                    };
                    //Debug.WriteLine(newUserFac);
                    _context.Add(newUserFac);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
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

            facilityManagementVM.Facility = _context.Facilities
               .Include(f => f.Company)
               .Where(m => m.Id == id);

            facilityManagementVM.FacilityChemicals = _context.FacilityChemicals
                .Include(fc => fc.Chemical)
                .Include(fc => fc.ChemicalState)
                .Where(f => f.FacilityId == id);

            facilityManagementVM.User = currentUser;

            var stuserFacilities = _context.StuserFacilities
                .Include(uf => uf.Facility)
                .Include(uf => uf.Stuser)
                .Where(u => u.FacilityId == facilityManagementVM.Facility.FirstOrDefault().Id);

            List<StuserMoreData> facEmployees = new List<StuserMoreData>();

            foreach (var item in stuserFacilities)
            {
                string indentityUserId = item.Stuser.AspnetIdentityId;
                IdentityUser identityUser = await _userManager.FindByIdAsync(indentityUserId);

                StuserMoreData stuserMoreData = new StuserMoreData
                {
                    Stuser = item.Stuser,
                    Email = identityUser.Email,
                    PhoneNumber = identityUser.PhoneNumber,
                    //Role = identityUser.
                };
                facEmployees.Add(stuserMoreData);
            }
            facilityManagementVM.FacilityEmployees = facEmployees;

            var usersFacilities = _context.StuserFacilities.Where(uf => uf.StuserId == currentUser.Id);

            if (facilityManagementVM == null)
            {
                return NotFound();
            }

            if (currentUser.CompanyId == facilityManagementVM.Facility.FirstOrDefault().CompanyId && usersFacilities.Any(uf => uf.FacilityId == id) || User.IsInRole("Admin"))
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
            // get the current users identity ID
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            string userId = claim.Value;

            // look up the current user in the spill tracker DB
            Stuser currentUser = _context.Stusers.Where(stu => stu.AspnetIdentityId == userId).FirstOrDefault();


            ViewData["CompanyId"] = currentUser.CompanyId;
            ViewData["States"] = new List<SelectListItem>
            {
                new SelectListItem() {Text="AL", Value="AL"},
                new SelectListItem() { Text="AK", Value="AK"},
                new SelectListItem() { Text="AZ", Value="AZ"},
                new SelectListItem() { Text="AR", Value="AR"},
                new SelectListItem() { Text="CA", Value="CA"},
                new SelectListItem() { Text="CO", Value="CO"},
                new SelectListItem() { Text="CT", Value="CT"},
                new SelectListItem() { Text="DC", Value="DC"},
                new SelectListItem() { Text="DE", Value="DE"},
                new SelectListItem() { Text="FL", Value="FL"},
                new SelectListItem() { Text="GA", Value="GA"},
                new SelectListItem() { Text="HI", Value="HI"},
                new SelectListItem() { Text="ID", Value="ID"},
                new SelectListItem() { Text="IL", Value="IL"},
                new SelectListItem() { Text="IN", Value="IN"},
                new SelectListItem() { Text="IA", Value="IA"},
                new SelectListItem() { Text="KS", Value="KS"},
                new SelectListItem() { Text="KY", Value="KY"},
                new SelectListItem() { Text="LA", Value="LA"},
                new SelectListItem() { Text="ME", Value="ME"},
                new SelectListItem() { Text="MD", Value="MD"},
                new SelectListItem() { Text="MA", Value="MA"},
                new SelectListItem() { Text="MI", Value="MI"},
                new SelectListItem() { Text="MN", Value="MN"},
                new SelectListItem() { Text="MS", Value="MS"},
                new SelectListItem() { Text="MO", Value="MO"},
                new SelectListItem() { Text="MT", Value="MT"},
                new SelectListItem() { Text="NE", Value="NE"},
                new SelectListItem() { Text="NV", Value="NV"},
                new SelectListItem() { Text="NH", Value="NH"},
                new SelectListItem() { Text="NJ", Value="NJ"},
                new SelectListItem() { Text="NM", Value="NM"},
                new SelectListItem() { Text="NY", Value="NY"},
                new SelectListItem() { Text="NC", Value="NC"},
                new SelectListItem() { Text="ND", Value="ND"},
                new SelectListItem() { Text="OH", Value="OH"},
                new SelectListItem() { Text="OK", Value="OK"},
                new SelectListItem() { Text="OR", Value="OR"},
                new SelectListItem() { Text="PA", Value="PA"},
                new SelectListItem() { Text="PR", Value="PR"},
                new SelectListItem() { Text="RI", Value="RI"},
                new SelectListItem() { Text="SC", Value="SC"},
                new SelectListItem() { Text="SD", Value="SD"},
                new SelectListItem() { Text="TN", Value="TN"},
                new SelectListItem() { Text="TX", Value="TX"},
                new SelectListItem() { Text="UT", Value="UT"},
                new SelectListItem() { Text="VT", Value="VT"},
                new SelectListItem() { Text="VA", Value="VA"},
                new SelectListItem() { Text="WA", Value="WA"},
                new SelectListItem() { Text="WV", Value="WV"},
                new SelectListItem() { Text="WI", Value="WI"},
                new SelectListItem() { Text="WY", Value="WY"}
            };

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
            // get the current users identity ID
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            string userId = claim.Value;

            // look up the current user in the spill tracker DB
            Stuser currentUser = _context.Stusers.Where(stu => stu.AspnetIdentityId == userId).FirstOrDefault();

            if (ModelState.IsValid)
            {
                //string location = getCoords(AddressStreet, AddressCity, AddressState, AddressZip);
                //facility.Location = location;
                var code = Guid.NewGuid().ToString();
                facility.AccessCode = code.ToUpper().Substring(26, 10);
                _context.Add(facility);
                await _context.SaveChangesAsync();

                StuserFacility newUserFacility = new StuserFacility
                {
                    FacilityId = _context.Facilities.Where(f => f.CompanyId == facility.CompanyId && f.AccessCode == facility.AccessCode).FirstOrDefault().Id,
                    StuserId = currentUser.Id
                };
                _context.Add(newUserFacility);
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
             ViewData["CompanyId"] = currentUser.CompanyId;
             ViewData["States"] = new List<SelectListItem>
            {
                new SelectListItem() {Text="AL", Value="AL"},
                new SelectListItem() { Text="AK", Value="AK"},
                new SelectListItem() { Text="AZ", Value="AZ"},
                new SelectListItem() { Text="AR", Value="AR"},
                new SelectListItem() { Text="CA", Value="CA"},
                new SelectListItem() { Text="CO", Value="CO"},
                new SelectListItem() { Text="CT", Value="CT"},
                new SelectListItem() { Text="DC", Value="DC"},
                new SelectListItem() { Text="DE", Value="DE"},
                new SelectListItem() { Text="FL", Value="FL"},
                new SelectListItem() { Text="GA", Value="GA"},
                new SelectListItem() { Text="HI", Value="HI"},
                new SelectListItem() { Text="ID", Value="ID"},
                new SelectListItem() { Text="IL", Value="IL"},
                new SelectListItem() { Text="IN", Value="IN"},
                new SelectListItem() { Text="IA", Value="IA"},
                new SelectListItem() { Text="KS", Value="KS"},
                new SelectListItem() { Text="KY", Value="KY"},
                new SelectListItem() { Text="LA", Value="LA"},
                new SelectListItem() { Text="ME", Value="ME"},
                new SelectListItem() { Text="MD", Value="MD"},
                new SelectListItem() { Text="MA", Value="MA"},
                new SelectListItem() { Text="MI", Value="MI"},
                new SelectListItem() { Text="MN", Value="MN"},
                new SelectListItem() { Text="MS", Value="MS"},
                new SelectListItem() { Text="MO", Value="MO"},
                new SelectListItem() { Text="MT", Value="MT"},
                new SelectListItem() { Text="NE", Value="NE"},
                new SelectListItem() { Text="NV", Value="NV"},
                new SelectListItem() { Text="NH", Value="NH"},
                new SelectListItem() { Text="NJ", Value="NJ"},
                new SelectListItem() { Text="NM", Value="NM"},
                new SelectListItem() { Text="NY", Value="NY"},
                new SelectListItem() { Text="NC", Value="NC"},
                new SelectListItem() { Text="ND", Value="ND"},
                new SelectListItem() { Text="OH", Value="OH"},
                new SelectListItem() { Text="OK", Value="OK"},
                new SelectListItem() { Text="OR", Value="OR"},
                new SelectListItem() { Text="PA", Value="PA"},
                new SelectListItem() { Text="PR", Value="PR"},
                new SelectListItem() { Text="RI", Value="RI"},
                new SelectListItem() { Text="SC", Value="SC"},
                new SelectListItem() { Text="SD", Value="SD"},
                new SelectListItem() { Text="TN", Value="TN"},
                new SelectListItem() { Text="TX", Value="TX"},
                new SelectListItem() { Text="UT", Value="UT"},
                new SelectListItem() { Text="VT", Value="VT"},
                new SelectListItem() { Text="VA", Value="VA"},
                new SelectListItem() { Text="WA", Value="WA"},
                new SelectListItem() { Text="WV", Value="WV"},
                new SelectListItem() { Text="WI", Value="WI"},
                new SelectListItem() { Text="WY", Value="WY"}
            };


            if (id == null)
            {
                return NotFound();
            }

            var facility = await _context.Facilities.FindAsync(id);
            if (facility == null)
            {
                return NotFound();
            }
            //ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", facility.CompanyId);

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
        [Authorize(Roles = "Admin, FacilityManager")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,AddressStreet,AddressCity,AddressState,AddressZip,Location,Industry,CompanyId,AccessCode")] Facility facility)
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

            ViewData["ChemicalStateId"] = new SelectList(_context.ChemicalStates, "Id", "Type");

            EditFacilityChemsVM chems = new EditFacilityChemsVM();

            chems.Chemicals = await _context.Chemicals.OrderBy(x => x.Name).ToListAsync();
            chems.FacilityChemicals = _context.FacilityChemicals
                .Include(fc => fc.Chemical)
                .Include(fc => fc.ChemicalState)
                .Where(f => f.FacilityId == id);
            chems.FacilityId = id;

            if (currentUser.CompanyId == facility.CompanyId || User.IsInRole("Admin"))
            {
                return View(chems);
            }
            else
            {
                return Redirect("~/Identity/Account/AccessDenied"); // need to return access denied because user tried accessing a company data they are not apart of 
            }
        }


        // POST: FacilityChemicals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, FacilityManager")]
        public async Task<IActionResult> CreateFacilityChem([Bind("Id,Concentration,ChemicalTemperature,ChemicalTemperatureUnits,ChemicalStateId,ChemicalId,FacilityId")] FacilityChemical facilityChemical)
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

        //string concetration, string chemTemp, string chemTempUnits, int chemStateId, int chemId, int facilityId
        public IActionResult SaveChemical(string chemData)
        {
            FacilityChemical newFacChem = new FacilityChemical();
            try
            {
                newFacChem = JsonConvert.DeserializeObject<FacilityChemical>(chemData);
              
                _context.FacilityChemicals.Add(newFacChem);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Response.StatusCode = 500;
            }
            return Json("coords: ");
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("RemoveEmployeeFromFacility")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveEmployeeFromFacility (int facilityId, int stuserId)
        {
            Debug.WriteLine($"facilityId: {facilityId}, stuserId: {stuserId}");

            var stuserFacility = await _context.StuserFacilities.Where(uf => uf.StuserId == stuserId && uf.FacilityId == facilityId).FirstOrDefaultAsync();
            _context.StuserFacilities.Remove(stuserFacility);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = facilityId });
        }
    }
}