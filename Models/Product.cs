using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriEnergyConnect.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Category { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly ProductionDate { get; set; }

        [ForeignKey("Farmer")]
        public int FarmerId { get; set; }

        public required Farmer Farmer { get; set; }
    }
}
