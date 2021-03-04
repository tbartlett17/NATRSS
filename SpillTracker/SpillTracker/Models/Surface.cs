using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SpillTracker.Models
{
    [Table("Surface")]
    public partial class Surface
    {
        public Surface()
        {
            Forms = new HashSet<Form>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(25)]
        public string Type { get; set; }

        [InverseProperty(nameof(Form.SpillSurface))]
        public virtual ICollection<Form> Forms { get; set; }
    }
}
