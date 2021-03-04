using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpillTracker.Models;
using HtmlAgilityPack;
using System.Diagnostics;

namespace SpillTracker.Controllers
{
    public class Status : Controller
    {
        private readonly SpillTrackerDbContext dbSpllTracker;
        internal DbSet<Chemical> chemicals;

        public Status(SpillTrackerDbContext context)
        {
            dbSpllTracker = context;
        }

        public IActionResult Index()
        {
            ScrapeEPCRAsite();
            ScrapeCERCLAsite();
            return View(dbSpllTracker.Chemicals);
        }
        

        public void ScrapeEPCRAsite()
        {
            /* sources:
             * https://www.technologycrowds.com/2020/06/how-to-parse-html-table-using-html-Agility-Pack-C-Sharp.html
             * https://html-agility-pack.net/
             */

            // Load the EPCRA site and get the table of Extremely Hazardous Substances
            string url = "https://www.ecfr.gov/cgi-bin/text-idx?SID=5bda0c1c4736b83aaf402bed85944e07&mc=true&node=pt40.30.355&rgn=div5#ap40.30.355_161.a";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            //IEnumerable<HtmlNode> nodes = doc.DocumentNode.SelectNodes("//table[contains(@class, 'gpotbl_table')]");
            IEnumerable<HtmlNode> nodes2 = doc.DocumentNode.SelectNodes("/html/body/div/div[2]/div[2]/div[43]/div[2]/table");
            //List<HtmlNode> nodes2 = nodes.ToList();
            //nodes2.RemoveRange(0, 1);
            //nodes2.RemoveAt(1);


            // Using LINQ to parse HTML table into a list of tabel rows 
            IEnumerable<HtmlNode> htmlTableRowsList = from table in nodes2
                                                      from row in table.SelectNodes("tr").Cast<HtmlNode>()
                                                      select row;


            bool skipTableHeader = true;
            foreach (HtmlNode row in htmlTableRowsList)
            {
                if(skipTableHeader)
                {
                    skipTableHeader = false;
                    continue;
                }

                // Using LINQ to parse HTML TR into list of cells
                IEnumerable<HtmlNode> trCells = from cell in row.SelectNodes("th|td").Cast<HtmlNode>()
                                                select cell;

                Debug.WriteLine(": " + trCells.ElementAt(0).InnerHtml);
                Debug.WriteLine(": " + trCells.ElementAt(1).InnerHtml);
                Debug.WriteLine(": " + trCells.ElementAt(3).InnerHtml);

                Chemical parsedChemical = new Chemical
                {
                    CasNum = trCells.ElementAt(0).InnerHtml,
                    Name = trCells.ElementAt(1).InnerHtml,
                    ReportableQuantity = Convert.ToDouble(trCells.ElementAt(3).InnerHtml),
                    ReportableQuantityUnits = "lbs",
                    EpcraChem = true
                };

                if (dbSpllTracker.Chemicals.Any(c => c.CasNum == parsedChemical.CasNum && c.Name == parsedChemical.Name && c.ReportableQuantity == parsedChemical.ReportableQuantity))
                {
                    Debug.WriteLine(parsedChemical.Name + " already exists in the database, skipping entry...");
                }
                else if (dbSpllTracker.Chemicals.Any(c => c.CasNum == parsedChemical.CasNum && c.Name == parsedChemical.Name && c.ReportableQuantity != parsedChemical.ReportableQuantity))
                {
                    Debug.WriteLine(parsedChemical.Name + " exists in the database but reportable quantity of " + parsedChemical.ReportableQuantity
                        + " lbs doesn't match the existing reportable quantity of " + dbSpllTracker.Chemicals.Where(c => c.CasNum == parsedChemical.CasNum).FirstOrDefault().ReportableQuantity 
                        + " lbs. Updating entry in database...");
                    dbSpllTracker.Chemicals.Where(c => c.CasNum == parsedChemical.CasNum).FirstOrDefault().ReportableQuantity = parsedChemical.ReportableQuantity;
                }
                else
                {
                    Debug.WriteLine(parsedChemical.Name + " doesn't exist in the database. Adding the substance to the list...");
                    dbSpllTracker.Add(parsedChemical);
                }
            }

            dbSpllTracker.SaveChanges();

        }


        public void ScrapeCERCLAsite()
        {
            string url = "https://www.ecfr.gov/cgi-bin/text-idx?SID=cf9016ebebd8898fcd57f71e1b66a7af&mc=true&node=se40.30.302_14&rgn=div8";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            IEnumerable<HtmlNode> nodes = doc.DocumentNode.SelectNodes("/html/body/div/div[2]/div[2]/div[4]/div[2]/table");


            // Using LINQ to parse HTML table into a list of tabel rows 
            IEnumerable<HtmlNode> htmlTableRowsList = from table in nodes
                                                      from row in table.SelectNodes("tr").Cast<HtmlNode>()
                                                      select row;


            bool skipTableHeader = true;
            foreach (HtmlNode row in htmlTableRowsList)
            {
                if (skipTableHeader)
                {
                    skipTableHeader = false;
                    continue;
                }

                // Using LINQ to parse HTML TR into list of cells
                IEnumerable<HtmlNode> trCells = from cell in row.SelectNodes("th|td").Cast<HtmlNode>()
                                                select cell;

                Debug.WriteLine(": " + trCells.ElementAt(0).InnerHtml);
                //string[] casStrArr = trCells.ElementAt(1).InnerHtml.Split()
                Debug.WriteLine(": " + trCells.ElementAt(1).InnerHtml.Replace("<br>", ", "));
                string[] rqStrArr = trCells.ElementAt(4).InnerHtml.Split('(');
                Debug.WriteLine(": " + rqStrArr[0]);

                if (String.IsNullOrEmpty(rqStrArr[0]))
                {
                    //Debug.WriteLine("\n\nno RQ listed, using parent RQ\n\n");
                    Chemical lastChem = dbSpllTracker.Chemicals.AsEnumerable().Last();
                    //Debug.WriteLine("\n\nlast chem: " + lastChem.Name + " , RQ: " + lastChem.ReportableQuantity + "\n\n");

                    Chemical parsedChem = new Chemical
                    {
                        CasNum = trCells.ElementAt(1).InnerHtml.Replace("<br>", ", "),
                        Name = trCells.ElementAt(0).InnerHtml,
                        ReportableQuantity = lastChem.ReportableQuantity,
                        ReportableQuantityUnits = "lbs",
                        CerclaChem = true
                    };

                    if (dbSpllTracker.Chemicals.Any(c => c.CasNum == parsedChem.CasNum && c.Name == parsedChem.Name && c.ReportableQuantity == parsedChem.ReportableQuantity))
                    {
                        Debug.WriteLine(parsedChem.Name + " already exists in the database, skipping entry...");
                    }
                    else if (dbSpllTracker.Chemicals.Any(c => c.CasNum == parsedChem.CasNum && c.Name == parsedChem.Name && c.ReportableQuantity != parsedChem.ReportableQuantity))
                    {
                        Debug.WriteLine(parsedChem.Name + " exists in the database but reportable quantity of " + parsedChem.ReportableQuantity
                            + " lbs doesn't match the existing reportable quantity of " + dbSpllTracker.Chemicals.Where(c => c.CasNum == parsedChem.CasNum).FirstOrDefault().ReportableQuantity
                            + " lbs. Updating entry in database...");
                        dbSpllTracker.Chemicals.Where(c => c.CasNum == parsedChem.CasNum).FirstOrDefault().ReportableQuantity = parsedChem.ReportableQuantity;
                    }
                    else
                    {
                        Debug.WriteLine(parsedChem.Name + " doesn't exist in the database. Adding the substance to the list...");
                        dbSpllTracker.Add(parsedChem);
                    }

                }
                else if (rqStrArr[0].Contains("**")) // ** means no rq for the broad term so skip this listing on the table
                {
                    Debug.WriteLine("\n\n skipping " + trCells.ElementAt(0).InnerHtml + " becasue RQ is **");
                    continue; 
                }
                else if (trCells.ElementAt(3).InnerHtml.Contains('D'))
                {
                    Debug.WriteLine("\n\n skipping " + trCells.ElementAt(0).InnerHtml);
                    continue;
                }
                else if (trCells.ElementAt(0).InnerHtml.Equals("Radionuclides (including radon)"))
                {
                    Debug.WriteLine("\n\n skipping " + trCells.ElementAt(0).InnerHtml);
                    continue;
                }
                else
                {
                    Chemical parsedChem = new Chemical();

                    if (trCells.ElementAt(0).InnerHtml.Equals("Mercurous nitrate"))
                    {
                        parsedChem.CasNum = trCells.ElementAt(1).InnerHtml + ", " + trCells.ElementAt(4).InnerHtml;
                        parsedChem.Name = trCells.ElementAt(0).InnerHtml;
                        string[] thisrq = trCells.ElementAt(3).InnerHtml.Split('(');
                        parsedChem.ReportableQuantity = Convert.ToDouble(thisrq[0]);
                        parsedChem.ReportableQuantityUnits = "lbs";
                        parsedChem.CerclaChem = true;
                    }
                    else
                    {
                        parsedChem.CasNum = trCells.ElementAt(1).InnerHtml.Replace("<br>", ", ");
                        parsedChem.Name = trCells.ElementAt(0).InnerHtml;
                        parsedChem.ReportableQuantity = Convert.ToDouble(rqStrArr[0]);
                        parsedChem.ReportableQuantityUnits = "lbs";
                        parsedChem.CerclaChem = true;
                    }
                    //Chemical parsedChem = new Chemical
                    //{
                    //    CasNum = trCells.ElementAt(1).InnerHtml.Replace("<br>", ", "),
                    //    Name = trCells.ElementAt(0).InnerHtml,
                    //    ReportableQuantity = Convert.ToDouble(rqStrArr[0]),
                    //    ReportableQuantityUnits = "lbs"
                    //};

                    if (dbSpllTracker.Chemicals.Any(c => c.CasNum == parsedChem.CasNum && c.Name == parsedChem.Name && c.ReportableQuantity == parsedChem.ReportableQuantity))
                    {
                        Debug.WriteLine(parsedChem.Name + " already exists in the database, skipping entry...");
                    }
                    else if (dbSpllTracker.Chemicals.Any(c => c.CasNum == parsedChem.CasNum && c.Name == parsedChem.Name && c.ReportableQuantity != parsedChem.ReportableQuantity))
                    {
                        Debug.WriteLine(parsedChem.Name + " exists in the database but reportable quantity of " + parsedChem.ReportableQuantity
                            + " lbs doesn't match the existing reportable quantity of " + dbSpllTracker.Chemicals.Where(c => c.CasNum == parsedChem.CasNum).FirstOrDefault().ReportableQuantity
                            + " lbs. Updating entry in database...");
                        dbSpllTracker.Chemicals.Where(c => c.CasNum == parsedChem.CasNum).FirstOrDefault().ReportableQuantity = parsedChem.ReportableQuantity;
                    }
                    else
                    {
                        Debug.WriteLine(parsedChem.Name + " doesn't exist in the database. Adding the substance to the list...");
                        dbSpllTracker.Add(parsedChem);
                    }
                }

               

                if (trCells.ElementAt(0).InnerHtml.Equals("Zirconium tetrachloride"))
                {
                    //Debug.WriteLine("\n\n found the last chem \n\n");
                    break; // last chemical we care about. break out of loop and stop capturing stuff on the table
                }
            }

                dbSpllTracker.SaveChanges();
                

        }

    }
}
