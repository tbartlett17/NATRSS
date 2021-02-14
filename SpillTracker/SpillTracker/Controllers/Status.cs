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

        public Status(SpillTrackerDbContext context)
        {
            dbSpllTracker = context;
        }


        public IActionResult Index()
        {
            ScrapeEPCRAsite();
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
                    ReportableQuantityUnits = "lbs"
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

            /*int loopCounter = 0;
            foreach (var cell in HTMLTableTRList)
            {
                //skip table headers
                if (loopCounter < 5)
                {
                    loopCounter++;
                    continue;
                }

                //skip the 4th cell
                if (loopCounter % 3 == 0)
                {

                }

                Debug.WriteLine("{0}: {1}", cell.Table_Name, cell.Cell_Text);
                loopCounter++;
                break;
            }
            */

            /*
            foreach (HtmlNode row in doc.DocumentNode.SelectNodes("/html/body/div/div[2]/div[2]/div[43]/div[2]/table/tbody/tr"))
            {
                HtmlNodeCollection cells = row.SelectNodes("td");
                for (int i = 0; i < cells.Count; ++i)
                {
                    switch (i)
                    {
                        case 0:
                            Debug.WriteLine(cells[i].InnerText);
                            break;
                        case 1:
                            Debug.WriteLine(cells[i].InnerText);
                            break;
                        case 2:
                            Debug.WriteLine(cells[i].InnerText);
                            break;
                        case 3:
                            Debug.WriteLine(cells[i].InnerText);
                            break;
                        case 4:
                            Debug.WriteLine(cells[i].InnerText);
                            break;
                        default:
                            Debug.WriteLine("something really bad happened");
                            break;
                    }
                }
            }
            */

            //foreach (HtmlNode node in nodes)
            //{
            //    Debug.WriteLine("Node Name: " + node.Name + "\n" + node.OuterHtml);
            //}


            //Debug.WriteLine("Node Name: " + node.Name + "\n" + node.OuterHtml);
        }

    }
}
