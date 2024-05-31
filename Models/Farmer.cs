using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriEnergyConnect.Models
{
    public class Farmer
    {
        [Key]
        public int FarmerId { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "The email is not the correct format")]
        public required string Email { get; set; }

        [ForeignKey("ApplicationUser")]
        public required string Id { get; set; }

        public required ApplicationUser ApplicationUser { get; set; }

    }
}
