using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SpillTracker.Models
{
    [Table("Facility")]
    public partial class Facility
    {
        public Facility()
        {
            FacilityChemicals = new HashSet<FacilityChemical>();
            Forms = new HashSet<Form>();
            StuserFacilities = new HashSet<StuserFacility>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [Column("Address_Street")]
        [StringLength(100)]
        public string AddressStreet { get; set; }
        [Column("Address_City")]
        [StringLength(100)]
        public string AddressCity { get; set; }
        [Column("Address_State")]
        [StringLength(50)]
        public string AddressState { get; set; }
        [Column("Address_ZIP")]
        [StringLength(15)]
        public string AddressZip { get; set; }
        [StringLength(100)]
        public string Location { get; set; }
        [StringLength(50)]
        public string Industry { get; set; }
        [StringLength(20)]
        public string AccessCode { get; set; }
        [Column("CompanyID")]
        public int? CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        [InverseProperty("Facilities")]
        public virtual Company Company { get; set; }
        [InverseProperty(nameof(FacilityChemical.Facility))]
        public virtual ICollection<FacilityChemical> FacilityChemicals { get; set; }
        [InverseProperty(nameof(Form.Facility))]
        public virtual ICollection<Form> Forms { get; set; }
        [InverseProperty(nameof(StuserFacility.Facility))]
        public virtual ICollection<StuserFacility> StuserFacilities { get; set; }
    }
}
