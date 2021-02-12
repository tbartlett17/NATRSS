using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpillTracker.Models
{
    [Table("Company")]
    public partial class Company
    {
        public Company()
        {
            Facilities = new HashSet<Facility>();
            Stusers = new HashSet<Stuser>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [Column("Num_Facilities")]
        public int? NumFacilities { get; set; }

        [InverseProperty(nameof(Facility.Company))]
        public virtual ICollection<Facility> Facilities { get; set; }
        [InverseProperty(nameof(Stuser.Company))]
        public virtual ICollection<Stuser> Stusers { get; set; }
    }
}
