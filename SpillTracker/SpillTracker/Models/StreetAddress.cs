using System.ComponentModel.DataAnnotations;

namespace SpillTracker.Models
{
    public class StreetAddress
    {
        
        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }
        [Required, MaxLength(2), MinLength(2)]
        public string State { get; set; }

        [Required, MaxLength(5), MinLength(5)]
        public string PostalCode { get; set; }
    }
}