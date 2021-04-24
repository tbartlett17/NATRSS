using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SpillTracker.Models;
using SpillTracker.Utilities;

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

        public IActionResult ByFIrstLetter(string l) 
        {
            //var list = new List<string> "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            var list = "A B C D E F G H I J K L M N O P Q R S T U V W X Y Z".Split(" ").ToList();
            var all = _context.Chemicals.OrderBy(x=>x.Name).ToList();
            var letter = new List<Chemical>();
            var hashtag = new List<Chemical>();
            letter = _context.Chemicals.Where(c => c.Name.Substring(0,1).Contains(l)).OrderBy(x => x.Name).ToList();
            hashtag = _context.Chemicals.Where(c => !list.Contains(c.Name.Substring(0,1))).OrderBy(x => x.Name).ToList();
            //_logger.LogInformation(sort.letterInput);(x => x.Name).ToList();
            
            if(l == null) 
            {
                return View(_context.Chemicals.OrderBy(x=>x.Name).ToListAsync()); 
            }
            else if(l.Length > 1)
            {
                letter = _context.Chemicals.Where(c => c.Name.Substring(0,l.Length).Contains(l)).OrderBy(x => x.Name).ToList();
                return View("Index", letter);
            }
            else if(l != "#") 
            {
                return View("Index", letter);
            }
            else if (l == "#")
            {
                return View("Index", hashtag); //needs adjustment
            }
            else
            {
               return View(_context.Chemicals.OrderBy(x=>x.Name).ToListAsync());   
            }   
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

            ExtraChemData extraData = GetCIDMolWeightFromPUGRest(chemical.CasNum);

            return View(chemical);
        }


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

        // //GET: Chemicals/Edit/5
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

        // //  POST: Chemicals/Edit/5
        // // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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



        //Attempt to get CID and Molecular weight from the Pug REst API
        public  ExtraChemData GetCIDMolWeightFromPUGRest(string casNumber)
        {
 
            string url;
            int cIDNumber;
            double molWeight;
            ExtraChemData currentData = new ExtraChemData 
            { 
                CID = 0, 
                MolecularWeight = 0, 
                Density = 0, 
                VaporPressure = 0, 
                Message = "The Pug Rest API could not find a specific compound associated with this CAS Number, There are possibly multiple compounds" +
                "associated with this CAS  number causing errors with the API call. Search CAS on https://pubchem.ncbi.nlm.nih.gov/"
            };

            url = $"https://pubchem.ncbi.nlm.nih.gov/rest/pug/compound/name/{casNumber}/property/MolecularWeight/json";
         
            //try to search for compound using the CID from Pug-Rest API If that doesn't work try again with CAS-prepended to the CAS number
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                string jsonString = null;
                // TODO: You should handle exceptions here
                using (WebResponse response = request.GetResponse())
                {
                    Stream stream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(stream);
                    jsonString = reader.ReadToEnd();
                    reader.Close();
                    stream.Close();
                }

                //The API call worked correctly now update the cid and molecular weight
                /* Debug.WriteLine(jsonString);*/
                JObject geo = JObject.Parse(jsonString);

                cIDNumber = (int)geo["PropertyTable"]["Properties"][0]["CID"];
                molWeight = (double)geo["PropertyTable"]["Properties"][0]["MolecularWeight"];
            }
            catch (Exception)
            {
                //Using the CID didn't work leave this function and continue to the next.
                Debug.WriteLine("try add 'CAS-' to the beginning of the cas number");
                cIDNumber = 0;
                molWeight = 0;
            }

            //Api format 
            if (cIDNumber==0)
            {
                try
                {
                    //try the same api call with the CAS number prepended by 'CAS-'
                    url = $"https://pubchem.ncbi.nlm.nih.gov/rest/pug/compound/name/cas-{casNumber}/property/MolecularWeight/json";
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    string jsonString = null;
                    // TODO: You should handle exceptions here
                    using (WebResponse response = request.GetResponse())
                    {
                        Stream stream = response.GetResponseStream();
                        StreamReader reader = new StreamReader(stream);
                        jsonString = reader.ReadToEnd();
                        reader.Close();
                        stream.Close();
                    }

                    //The API call worked correctly now update the cid and molecular weight
                    Debug.WriteLine(jsonString);
                    JObject geo = JObject.Parse(jsonString);

                    cIDNumber = (int)geo["PropertyTable"]["Properties"][0]["CID"];
                    molWeight = (double)geo["PropertyTable"]["Properties"][0]["MolecularWeight"];

                }
                catch (Exception)
                {
                    //This API call still did not work; There must be multiple compounds with the same CAS number, continue the function
                    Debug.WriteLine("There may be multiple compounds associated with this CAS search PubChem for CID");
                    cIDNumber = 0;
                    molWeight = 0;
                }
            }
            
            //If there is no cid number found by the API send back and empty extraChemData object to the controller
            if (cIDNumber != 0) 
            { 
                currentData = GetDensVapPresFromPUGView(cIDNumber, casNumber, molWeight); 
            }

            if (_context.Chemicals.Where(a => a.CasNum == casNumber).Select(x => x.PubChemCid).FirstOrDefault() != cIDNumber)
            {
                Chemical chem = _context.Chemicals.Where(a => a.CasNum == casNumber).First();
                chem.PubChemCid = cIDNumber;
                chem.MolecularWeight = molWeight;
                chem.MolecularWeightUnits = "g/mol";
                _context.SaveChanges();

            }

            return currentData;      
        }

     

        public ExtraChemData GetDensVapPresFromPUGView(int cIDNumber, string casNumber, double molecweight)
        {
            string jsonString;
            string jsonString2;
            string url;
            string url2;
            string densityString;
            string vaporPressureString;
            double? density;
            double? vaporPressure; 
            Chemical C = _context.Chemicals.Where(c => c.CasNum == casNumber).FirstOrDefault();
            url = $"https://pubchem.ncbi.nlm.nih.gov/rest/pug_view/data/compound/{cIDNumber}/JSON?heading=Density";
            url2 = $"https://pubchem.ncbi.nlm.nih.gov/rest/pug_view/data/compound/{cIDNumber}/JSON?heading=Vapor+Pressure";

            //try to send an API call to the PugView API for Density and Vapor Pressure
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                // TODO: You should handle exceptions here
                using (WebResponse response = request.GetResponse())
                {
                    Stream stream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(stream);
                    jsonString = reader.ReadToEnd();

                    reader.Close();
                    stream.Close();
                }
                //The call was successful populate the density and vapor pressure 
                JObject geo = JObject.Parse(jsonString);
                densityString = (string)geo["Record"]["Section"][0]["Section"][0]["Section"][0]["Information"][0]["Value"]["StringWithMarkup"][0]["String"];

                density = RegexParserUtilities.RegexDensityParse(densityString); double.Parse(Regex.Match(densityString, @"^\d*\.*\d*").Value);

                string densitytest = Regex.Match(densityString, @"^(\d*\.*\d*)-(\d*\.*\d*)").Value;
                string densitytest2 = Regex.Match(densityString, @"^\d*\.*\d*").Value;
                if (densitytest != "")
                {

                    density = double.Parse(Regex.Match(densityString, @"^(\d*\.*\d*)-(\d*\.*\d*)").Groups[2].Value);

                }
            }
            catch (Exception)
            {
                //API call failed set the density to 0
                density = -1;
                Debug.WriteLine("density not found");
            }

            //try to send an API call to the PugView API for Vapor Pressure
            try
            {
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(url2);
                // TODO: You should handle exceptions here
                using (WebResponse response = request2.GetResponse())
                {
                    Stream stream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(stream);
                    jsonString2 = reader.ReadToEnd();
                    reader.Close();
                    stream.Close();
                }
                /* Debug.WriteLine(jsonString);*/
                //The call was successful populate the vapor pressure 
                JObject geo2 = JObject.Parse(jsonString2);

                //System.Text.RegularExpressions.Regex  
                vaporPressureString = (string)geo2["Record"]["Section"][0]["Section"][0]["Section"][0]["Information"][0]["Value"]["StringWithMarkup"][0]["String"];
                vaporPressureString = vaporPressureString.Replace("X10-", "e-0");
                vaporPressure = (double)Decimal.Parse(Regex.Match(vaporPressureString, @"^\d*\.*\d*e*-*\d*").Value, NumberStyles.Float);
              
            }
            catch (Exception)
            {
                //API call failed set the vapor pressure to 0
                vaporPressure = -1;
                Debug.WriteLine("vapor Pressure not found");
            }

            if (_context.Chemicals.Where(a => a.CasNum == casNumber).Select(x => x.Density).FirstOrDefault() != density || _context.Chemicals.Where(a => a.CasNum == casNumber).Select(x => x.VaporPressure).FirstOrDefault() != vaporPressure )
            {
                Chemical chem = _context.Chemicals.Where(a => a.CasNum == casNumber).First();
                chem.Density = density;
                chem.VaporPressure = vaporPressure;             
                _context.SaveChanges();

            }
            return (new ExtraChemData { CID = cIDNumber, MolecularWeight = molecweight, Density = (double)density, VaporPressure = (double)vaporPressure, Message = "Success" });

        }
    }
}
