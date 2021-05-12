using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SpillTracker.Models
{
    public partial class StuserFacility
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("StuserID")]
        public int? StuserId { get; set; }
        [Column("FacilityID")]
        public int? FacilityId { get; set; }

        [ForeignKey(nameof(FacilityId))]
        [InverseProperty("StuserFacilities")]
        public virtual Facility Facility { get; set; }
        [ForeignKey(nameof(StuserId))]
        [InverseProperty("StuserFacilities")]
        public virtual Stuser Stuser { get; set; }
    }
}
