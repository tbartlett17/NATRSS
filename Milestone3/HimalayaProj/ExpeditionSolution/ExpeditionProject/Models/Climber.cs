using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ExpeditionProject.Models
{
    [Table("Climber")]
    public partial class Climber
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }
        public int? Age { get; set; }
        [Column("ExpeditionID")]
        public int? ExpeditionId { get; set; }

        [ForeignKey(nameof(ExpeditionId))]
        [InverseProperty("Climbers")]
        public virtual Expedition Expedition { get; set; }
    }
}
