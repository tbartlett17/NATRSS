using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SpillTracker.Models
{
    public partial class FacilityChemical
    {
        public FacilityChemical()
        {
            Forms = new HashSet<Form>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
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
        [ForeignKey(nameof(FacilityId))]
        [InverseProperty("FacilityChemicals")]
        public virtual Facility Facility { get; set; }
        [InverseProperty(nameof(Form.FacilityChemical))]
        public virtual ICollection<Form> Forms { get; set; }
    }
}
