using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpillTracker.Models
{
    public class FacilityManagementVM
    {
        public IEnumerable<Facility> Facility { get; set; }

        public IEnumerable<FacilityChemical> FacilityChemicals { get; set; }

        public Stuser User {get; set;}

        public IEnumerable<StuserMoreData> FacilityEmployees { get; set; }

        public string Codes {get; set;}
    }
}
