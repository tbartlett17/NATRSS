using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SpillTracker.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;

namespace SpillTracker.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly SpillTrackerDbContext dbSpllTracker;
        


        public AdminController(SpillTrackerDbContext context)
        {
            dbSpllTracker = context;
        }

        public IActionResult Index()
        {
            //ScrapeEPCRAtable();
            return View();
        }

         // GET: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NumFacilities,AccessCode")] Company company)
        {
            
            
            if (ModelState.IsValid)
            {
                var code = Guid.NewGuid().ToString();
                company.AccessCode = code.ToUpper().Substring(26, 10);
                dbSpllTracker.Add(company);
                await dbSpllTracker.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        public IActionResult Exist(string name) 
        {
            Debug.WriteLine(name);
            if(dbSpllTracker.Companies.Any(n => n.Name == name))
            {
                Debug.WriteLine("true");
                return Json(true);
            }
            else 
            {
                Debug.WriteLine("false");
                return Json(false);
            }
        }


        public IActionResult ScrapeEPCRAtable(string xpath)
        {
            // Load the EPCRA site and get the table of Extremely Hazardous Substances
            string url = "https://www.ecfr.gov/cgi-bin/text-idx?SID=5bda0c1c4736b83aaf402bed85944e07&mc=true&node=pt40.30.355&rgn=div5#ap40.30.355_161.a";
            string defaultXpath = "/html/body/div/div[2]/div[2]/div[43]/div[2]/table";
            HtmlWeb web = new HtmlWeb();

            //Debug.WriteLine("\n\n xpath: " + xpath + "\n\n");

            try
            {
                // Load the webpage and use LINQ to parse HTML table into a list of tabel rows 
                HtmlDocument doc = web.Load(url);
                IEnumerable<HtmlNode> nodes = doc.DocumentNode.SelectNodes(xpath);
                IEnumerable<HtmlNode> htmlTableRowsList = from table in nodes
                                                          from row in table.SelectNodes("tr").Cast<HtmlNode>()
                                                          select row;

                bool skipTableHeader = true;
                foreach (HtmlNode row in htmlTableRowsList)
                {
                    if (skipTableHeader == true)
                    {
                        skipTableHeader = false;
                        continue;
                    }

                    // Use LINQ to parse HTML TR into list of cells
                    IEnumerable<HtmlNode> trCells = from cell in row.SelectNodes("th|td").Cast<HtmlNode>() select cell;

                    // Pull out relevant data from the table row
                    string parsedCas = String.Concat(trCells.ElementAt(0).InnerHtml.Where(c => !Char.IsWhiteSpace(c)));
                    string parsedName = trCells.ElementAt(1).InnerHtml;
                    double parsedRQ = Convert.ToDouble(trCells.ElementAt(3).InnerHtml);
                    //double parsedRQ, value;
                    //if (double.TryParse(trCells.ElementAt(3).InnerHtml, out value))
                    //{
                    //    parsedRQ = value;
                    //}

                    Chemical parsedChem = new Chemical
                    {
                        CasNum = parsedCas,
                        Name = parsedName,
                        ReportableQuantity = parsedRQ,
                        ReportableQuantityUnits = "lbs",
                        DensityUnits = "g/cm\u00B3",
                        MolecularWeightUnits = "g/mol",
                        VaporPressureUnits = "mm Hg",
                        EpcraChem = true
                    };

                    ProcessChemical(parsedChem);
                }

                UpdateStatusTime("EPCRA Scraper");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Response.StatusCode = 500;

            }
            return Json(true);
        }

        public IActionResult ScrapeCERCLAtable(string xpath)
        {
            // Load the CERCLA site and get the table of Hazardous Substances
            string url = "https://www.ecfr.gov/cgi-bin/text-idx?SID=cf9016ebebd8898fcd57f71e1b66a7af&mc=true&node=se40.30.302_14&rgn=div8";
            HtmlWeb web = new HtmlWeb();
            string defaultXpath = "/html/body/div/div[2]/div[2]/div[4]/div[2]/table";
            try
            {
                // Load the webpage and use LINQ to parse HTML table into a list of tabel rows 
                HtmlDocument doc = web.Load(url);
                IEnumerable<HtmlNode> nodes = doc.DocumentNode.SelectNodes(xpath);
                IEnumerable<HtmlNode> htmlTableRowsList = from table in nodes
                                                          from row in table.SelectNodes("tr").Cast<HtmlNode>()
                                                          select row;
                bool skipTableHeader = true;
                foreach (HtmlNode row in htmlTableRowsList)
                {
                    if (skipTableHeader == true)
                    {
                        skipTableHeader = false;
                        continue;
                    }

                    // Use LINQ to parse HTML TR into list of cells
                    IEnumerable<HtmlNode> trCells = from cell in row.SelectNodes("th|td").Cast<HtmlNode>() select cell;

                    // Filter the RQ column to start parsing around the table
                    string[] rqStrArr = trCells.ElementAt(4).InnerHtml.Split('('); // Filters to [0] lbs value and [1] kg value
                    
                    if (String.IsNullOrEmpty(rqStrArr[0])) // When the RQ is blank, its assumed the chemical is a sublisting of some parent and thus use the RQ of the parent
                    {
                        // Pull out relevant data from the table row 
                        string parsedName = trCells.ElementAt(0).InnerHtml;
                        string[] casNumArr = trCells.ElementAt(1).InnerHtml.Split("<br>"); // For chems with many CAS nums listed. splits them in a list to be processed individually 
                        
                        Chemical lastChem = dbSpllTracker.Chemicals.AsEnumerable().Last();

                        foreach (string casNum in casNumArr)
                        {
                            Chemical parsedChem = new Chemical
                            {
                                CasNum = String.Concat(casNum.Where(c => !Char.IsWhiteSpace(c))),
                                Name = parsedName,
                                ReportableQuantity = lastChem.ReportableQuantity,
                                ReportableQuantityUnits = "lbs",
                                DensityUnits = "g/cm\u00B3",
                                MolecularWeightUnits = "g/mol",
                                VaporPressureUnits = "mm Hg",
                                CerclaChem = true
                            };

                            ProcessChemical(parsedChem);
                        }
                    }
                    else if (rqStrArr[0].Contains("**")) // ** means no rq for the broad term so skip this listing on the table
                    {
                        Debug.WriteLine("\n\n skipping " + trCells.ElementAt(0).InnerHtml + " becasue RQ is **");
                        continue;
                    }
                    else if (trCells.ElementAt(3).InnerHtml.Contains('D')) // Should filter out section of table showing all Unlisted Hazardous Wastes Characteristics of Corrosivity, Ignitability, Reactivity, and Toxicity which would all be duplicates on the table
                    {
                        Debug.WriteLine("\n\n skipping " + trCells.ElementAt(0).InnerHtml);
                        continue;
                    }
                    else if (trCells.ElementAt(0).InnerHtml.Contains("Radionuclides (including radon)")) // This row refers to Appendix B listing all Radionuclides. This individual row should be skipped
                    {
                        Debug.WriteLine("\n\n skipping " + trCells.ElementAt(0).InnerHtml);
                        continue;
                    }
                    else
                    {
                        // Pull out relevant data from the table row 
                        double parsedRQ = Convert.ToDouble(rqStrArr[0]);
                        string parsedName = trCells.ElementAt(0).InnerHtml;
                        string[] casNumArr = trCells.ElementAt(1).InnerHtml.Split("<br>"); // For chems with many CAS nums listed. splits them in a list to be processed individually 

                        foreach (string casNum in casNumArr)
                        {
                            Chemical parsedChem = new Chemical
                            {
                                CasNum = String.Concat(casNum.Where(c => !Char.IsWhiteSpace(c))),
                                Name = parsedName,
                                ReportableQuantity = parsedRQ,
                                ReportableQuantityUnits = "lbs",
                                DensityUnits = "g/cm\u00B3",
                                MolecularWeightUnits = "g/mol",
                                VaporPressureUnits = "mm Hg",
                                CerclaChem = true
                            };

                            ProcessChemical(parsedChem);
                        }
                    }

                    if (trCells.ElementAt(0).InnerHtml.Equals("Zirconium tetrachloride"))
                    {
                        break; // last chemical we care about. break out of loop and stop capturing data on the table
                    }
                }

                UpdateStatusTime("CERCLA Scraper");
            }
            catch (Exception ex) // change status code 
            {
                Debug.WriteLine(ex);
                Response.StatusCode = 500;
            }

            return Json(true);
        }

        public void ProcessChemical(Chemical parsedChem)
        {
            if (parsedChem.Name.Contains("&prime;"))
            {
                parsedChem.Name = parsedChem.Name.Replace("&prime;", "\u2032");
            }

            if (parsedChem.Name.Contains("&#961;"))
            {
                parsedChem.Name = parsedChem.Name.Replace("&#961;", "\u2CA3");
            }

            if (parsedChem.Name.Contains("&#945;"))
            {
                parsedChem.Name = parsedChem.Name.Replace("&#945;", "\u03B1");
            }

            if (parsedChem.Name.Contains("&#946;"))
            {
                parsedChem.Name = parsedChem.Name.Replace("&#946;", "\u03B2");
            }

            if (parsedChem.Name.Contains("&dagger;"))
            {
                parsedChem.Name = parsedChem.Name.Replace("&dagger;", "");
            }

            if (parsedChem.Name.Contains("&amp;"))
            {
                parsedChem.Name = parsedChem.Name.Replace("&amp;", "&");
            }

            if (parsedChem.Name.Contains("&gt;"))
            {
                parsedChem.Name = parsedChem.Name.Replace("&gt;", ">");
            }

            if (parsedChem.Name.Contains("&lt;"))
            {
                parsedChem.Name = parsedChem.Name.Replace("&lt;", "<");
            }

            if (parsedChem.Name.Contains("&nbsp;"))
            {
                parsedChem.Name = parsedChem.Name.Replace("&nbsp;", "");
            }

            if (parsedChem.Name.Contains("&mdash;"))
            {
                parsedChem.Name = parsedChem.Name.Replace("&mdash;", "\u2014");
            }

            if (parsedChem.Name.Contains("<sup>a</sup>"))
            {
                parsedChem.Name = parsedChem.Name.Replace("<sup>a</sup>", "");
            }

            if (parsedChem.Name.Contains("<sup>b</sup>"))
            {
                parsedChem.Name = parsedChem.Name.Replace("<sup>b</sup>", "");
            }

            if (parsedChem.Name.Contains("<sup>c</sup>"))
            {
                parsedChem.Name = parsedChem.Name.Replace("<sup>c</sup>", "");
            }

            if (parsedChem.Name.Contains("<sup>d</sup>"))
            {
                parsedChem.Name = parsedChem.Name.Replace("<sup>d</sup>", "");
            }

            if (parsedChem.Name.Contains("<sup>e</sup>"))
            {
                parsedChem.Name = parsedChem.Name.Replace("<sup>e</sup>", "");
            }

            if (parsedChem.Name.Contains("<sup>f</sup>"))
            {
                parsedChem.Name = parsedChem.Name.Replace("<sup>f</sup>", "");
            }

            if (parsedChem.Name.Contains("(Na(N < span style = \"font - size:70 %; vertical - align:sub\" > 3 </ span >))"))
            {
                parsedChem.Name = parsedChem.Name.Replace("(Na(N < span style = \"font - size:70 %; vertical - align:sub\" > 3 </ span >))", "");
            }

            

            if (parsedChem.CasNum.Contains("&nbsp;&nbsp;&nbsp;")) // delete these whitespace html values
            {
                parsedChem.CasNum = null;
                //Debug.WriteLine("found one");
            }

            if (!String.IsNullOrEmpty(parsedChem.CasNum) && !parsedChem.CasNum.Contains("-")) // cas num needs to get formatted properly. ex: change xxxxyyz to xxxx-yy-z
            {
                parsedChem.CasNum = parsedChem.CasNum.Insert((parsedChem.CasNum.Length - 1), "-");
                parsedChem.CasNum = parsedChem.CasNum.Insert((parsedChem.CasNum.Length - 4), "-");
                //Debug.WriteLine(parsedChem.CasNum);
            }

            if (dbSpllTracker.Chemicals.Any(c => c.CasNum == parsedChem.CasNum && c.Name.ToUpper() == parsedChem.Name.ToUpper() && c.ReportableQuantity == parsedChem.ReportableQuantity))
            {
                Debug.WriteLine("{{{ NAME:" + parsedChem.Name + ", CAS:" + parsedChem.CasNum + ", RQ:" + parsedChem.ReportableQuantity + "}}} exists in the database, skipping entry...");
            }
            else if (dbSpllTracker.Chemicals.Any(c => c.CasNum == parsedChem.CasNum && c.Name.ToUpper() == parsedChem.Name.ToUpper() && c.ReportableQuantity != parsedChem.ReportableQuantity))
            {
                Debug.WriteLine("{{{ NAME:" + parsedChem.Name + ", CAS:" + parsedChem.CasNum + "}}} exists in the database but reportable quantity of " + parsedChem.ReportableQuantity
                    + " lbs doesn't match the existing reportable quantity of " + dbSpllTracker.Chemicals.Where(c => c.CasNum == parsedChem.CasNum).FirstOrDefault().ReportableQuantity
                    + " lbs. Updating RQ in database...");
                dbSpllTracker.Chemicals.Where(c => c.CasNum == parsedChem.CasNum).FirstOrDefault().ReportableQuantity = parsedChem.ReportableQuantity;
            }
            else if (dbSpllTracker.Chemicals.Any(c => c.CasNum == parsedChem.CasNum && c.ReportableQuantity == parsedChem.ReportableQuantity && c.Name.ToUpper() != parsedChem.Name.ToUpper()))
            {
                Debug.WriteLine("{{{ CAS:" + parsedChem.CasNum + ", RQ:" + parsedChem.ReportableQuantity + "}}} exists in the database but its name, " + parsedChem.Name + ", did not match the existing name,"
                    + dbSpllTracker.Chemicals.Where(c => c.CasNum == parsedChem.CasNum).FirstOrDefault().Name + ". Adding " + parsedChem.Name
                    + " as a synonym to chemical " + parsedChem.CasNum);

                if (String.IsNullOrEmpty(dbSpllTracker.Chemicals.Where(c => c.CasNum == parsedChem.CasNum).FirstOrDefault().Aliases))
                {
                    if (parsedChem.Name.Length < dbSpllTracker.Chemicals.Where(c => c.CasNum == parsedChem.CasNum).FirstOrDefault().Name.Length)
                    {
                        string temp = dbSpllTracker.Chemicals.Where(c => c.CasNum == parsedChem.CasNum).FirstOrDefault().Name;
                        dbSpllTracker.Chemicals.Where(c => c.CasNum == parsedChem.CasNum).FirstOrDefault().Name = parsedChem.Name;
                        dbSpllTracker.Chemicals.Where(c => c.CasNum == parsedChem.CasNum).FirstOrDefault().Aliases += temp + "<br>";
                    }
                    else
                    {
                        dbSpllTracker.Chemicals.Where(c => c.CasNum == parsedChem.CasNum).FirstOrDefault().Aliases += parsedChem.Name + "<br>";
                    }
                }
                else
                {
                    string[] aliasArr = dbSpllTracker.Chemicals.Where(c => c.CasNum == parsedChem.CasNum).FirstOrDefault().Aliases.Split("<br>");

                    if (aliasArr.Contains(parsedChem.Name) == false)
                    {
                        if (parsedChem.EpcraChem == true && dbSpllTracker.Chemicals.Where(c => c.CasNum == parsedChem.CasNum).FirstOrDefault().EpcraChem == false)
                        {
                            dbSpllTracker.Chemicals.Where(c => c.CasNum == parsedChem.CasNum).FirstOrDefault().EpcraChem = true;
                        }

                        if (parsedChem.CerclaChem == true && dbSpllTracker.Chemicals.Where(c => c.CasNum == parsedChem.CasNum).FirstOrDefault().CerclaChem == false)
                        {
                            dbSpllTracker.Chemicals.Where(c => c.CasNum == parsedChem.CasNum).FirstOrDefault().CerclaChem = true;
                        }

                        foreach (string alias in aliasArr)
                        {
                            if (alias.ToUpper() == parsedChem.Name.ToUpper())
                            {
                                // Do nothing, chemical is a duplicate alias
                            }
                            else
                            {
                                if (parsedChem.Name.Length < dbSpllTracker.Chemicals.Where(c => c.CasNum == parsedChem.CasNum).FirstOrDefault().Name.Length)
                                {
                                    string temp = dbSpllTracker.Chemicals.Where(c => c.CasNum == parsedChem.CasNum).FirstOrDefault().Name;
                                    dbSpllTracker.Chemicals.Where(c => c.CasNum == parsedChem.CasNum).FirstOrDefault().Name = parsedChem.Name;
                                    dbSpllTracker.Chemicals.Where(c => c.CasNum == parsedChem.CasNum).FirstOrDefault().Aliases += temp + "<br>";
                                }
                                else
                                {
                                    dbSpllTracker.Chemicals.Where(c => c.CasNum == parsedChem.CasNum).FirstOrDefault().Aliases += parsedChem.Name + "<br>";
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Debug.WriteLine("{{{ NAME:" + parsedChem.Name + ", CAS:" + parsedChem.CasNum + ", RQ:" + parsedChem.ReportableQuantity + "}}} doesn't exist in the database. Adding the substance to the list...");
                dbSpllTracker.Add(parsedChem);
            }
            dbSpllTracker.SaveChanges();
        }

        public void UpdateStatusTime(string source)
        {
            StatusTime newStatusTime = new StatusTime();
            newStatusTime.SourceName = source;
            newStatusTime.Time = DateTime.UtcNow;

            dbSpllTracker.StatusTimes.Add(newStatusTime);
            dbSpllTracker.SaveChanges();
        }




    }

    
}
