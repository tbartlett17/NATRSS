using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpillTracker.Models
{
    public class EditFacilityChemsVM
    {
        public IEnumerable<Chemical> Chemicals { get; set; }

        public IEnumerable<FacilityChemical> FacilityChemicals { get; set; }

        public FacilityChemical? SelectedChem { get; set; }
    }
}
