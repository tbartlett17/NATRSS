using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpillTracker.Models
{
    [Table("STUser")]
    public partial class Stuser
    {
        public Stuser()
        {
            Forms = new HashSet<Form>();
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
    }
}
