using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ExpeditionProject.Models
{
    [Table("User")]
    public partial class User
    {
        public User()
        {
            BlogPosts = new HashSet<BlogPost>();
            Forms = new HashSet<Form>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }
        [Required]
        [StringLength(25)]
        public string Password { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }
        [Column("UserTypeID")]
        public int? UserTypeId { get; set; }

        [ForeignKey(nameof(UserTypeId))]
        [InverseProperty("Users")]
        public virtual UserType UserType { get; set; }
        [InverseProperty(nameof(BlogPost.User))]
        public virtual ICollection<BlogPost> BlogPosts { get; set; }
        [InverseProperty(nameof(Form.User))]
        public virtual ICollection<Form> Forms { get; set; }
    }
}
