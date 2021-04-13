using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpillTracker.Models
{
    public class FacilityManagementVM
    {
        public Facility Facility { get; set; }

        public IEnumerable<FacilityChemical> FacilityChemicals { get; set; }


    }
}
