using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ExpeditionProject.Models
{
    [Table("Form")]
    public partial class Form
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [StringLength(50)]
        public string Description { get; set; }
        [Column("ExpeditionID")]
        public int? ExpeditionId { get; set; }

        [Column("UserID")]
        public int? UserId { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        [StringLength(20)]
        public bool? Completed { get; set; }

        [ForeignKey(nameof(ExpeditionId))]
        [InverseProperty("Forms")]
        public virtual Expedition Expedition { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Forms")]
        public virtual User User { get; set; }

        [Column(TypeName = "date")]
        public DateTime? SubmissionDateTime { get; set; }
    }
}
