using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SpillTracker.Models
{
    [Table("StatusTime")]
    public partial class StatusTime
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        
        [Column("SourceName")]
        public string SourceName { get; set; }
        [StringLength(100)]

        [Column("Time",TypeName = "datetime")]
        public DateTime? Time { get; set; }
    }
}
