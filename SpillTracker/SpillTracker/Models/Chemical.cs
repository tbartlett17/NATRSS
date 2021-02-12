using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpillTracker.Models
{
    [Table("Chemical")]
    public partial class Chemical
    {
        public Chemical()
        {
            FacilityChemicals = new HashSet<FacilityChemical>();
            Forms = new HashSet<Form>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [Column("CAS_Num")]
        [StringLength(15)]
        public string CasNum { get; set; }
        [Column("PubChemCID")]
        public int? PubChemCid { get; set; }
        [Column("Reportable_Quantity")]
        public double? ReportableQuantity { get; set; }
        [Column("Reportable_Quantity_Units")]
        [StringLength(30)]
        public string ReportableQuantityUnits { get; set; }
        public double? Density { get; set; }
        [Column("Density_Units")]
        [StringLength(30)]
        public string DensityUnits { get; set; }
        [Column("Molecular_Weight")]
        public double? MolecularWeight { get; set; }
        [Column("Molecular_Weight_Units")]
        [StringLength(30)]
        public string MolecularWeightUnits { get; set; }
        [Column("Vapor_Pressure")]
        public double? VaporPressure { get; set; }
        [Column("Vapor_Pressure_Units")]
        [StringLength(30)]
        public string VaporPressureUnits { get; set; }
        [Column("CERCLA_Chem")]
        public bool? CerclaChem { get; set; }
        [Column("EPCRA_Chem")]
        public bool? EpcraChem { get; set; }

        [InverseProperty(nameof(FacilityChemical.Chemical))]
        public virtual ICollection<FacilityChemical> FacilityChemicals { get; set; }
        [InverseProperty(nameof(Form.Chemical))]
        public virtual ICollection<Form> Forms { get; set; }
    }
}
