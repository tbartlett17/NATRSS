using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SpillTracker.Models
{
    [Table("STUser")]
    public partial class Stuser
    {
        public Stuser()
        {
            Forms = new HashSet<Form>();
            StuserFacilities = new HashSet<StuserFacility>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("ASPNetIdentityID")]
        [StringLength(450)]
        public string AspnetIdentityId { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(25)]
        public string EmployeeNumber { get; set; }
        [Column("CompanyID")]
        public int? CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        [InverseProperty("Stusers")]
        public virtual Company Company { get; set; }
        [InverseProperty(nameof(Form.Stuser))]
        public virtual ICollection<Form> Forms { get; set; }
        [InverseProperty(nameof(StuserFacility.Stuser))]
        public virtual ICollection<StuserFacility> StuserFacilities { get; set; }
    }
}
