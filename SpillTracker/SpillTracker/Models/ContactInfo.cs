using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpillTracker.Models
{
    [Table("ContactInfo")]
    public partial class ContactInfo
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(150)]
        public string AgencyName { get; set; }
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        [StringLength(35)]
        public string State { get; set; }
    }
}
