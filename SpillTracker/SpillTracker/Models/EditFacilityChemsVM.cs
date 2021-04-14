using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpillTracker.Models
{
    public class EditFacilityChemsVM
    {
        public IEnumerable<Chemical> Chemicals { get; set; }

        public IEnumerable<FacilityChemical> FacilityChemicals { get; set; }

        public FacilityChemical SelectedChem { get; set; }

        public double? Concentration { get; set; }
        [Column("Chemical_Temperature")]
        public double? ChemicalTemperature { get; set; }
        [Column("Chemical_Temperature_Units")]
        [StringLength(30)]
        public string ChemicalTemperatureUnits { get; set; }
        [Column("ChemicalStateID")]
        public int? ChemicalStateId { get; set; }
        [Column("ChemicalID")]
        public int? ChemicalId { get; set; }
        [Column("FacilityID")]

        public int? FacilityId { get; set; }

        [ForeignKey(nameof(ChemicalId))]
        [InverseProperty("FacilityChemicals")]
        public virtual Chemical Chemical { get; set; }
        [ForeignKey(nameof(ChemicalStateId))]
        [InverseProperty("FacilityChemicals")]
        public virtual ChemicalState ChemicalState { get; set; }

    }
}
