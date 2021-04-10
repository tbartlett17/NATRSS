using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ExpeditionProject.Models
{
    [Table("BlogPost")]
    public partial class BlogPost
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(500)]
        public string Post { get; set; }
        [Column("UserID")]
        public int? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("BlogPosts")]
        public virtual User User { get; set; }
    }
}
