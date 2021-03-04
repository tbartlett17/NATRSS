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


        [Column("CERCLAScraperTime",TypeName = "datetime")]
        public DateTime? CerclaScraperTime { get; set; }


        [Column("EPCRAScraperTime", TypeName = "datetime")]
        public DateTime? EpcrascraperTime { get; set; }


        [Column("PubChemAPITime", TypeName = "datetime")]
        public DateTime? PubChemApitime { get; set; }
    }
}
