using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriEnergyConnect.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [ForeignKey("ApplicationUser")]
        public required string Id { get; set; }

        public required ApplicationUser ApplicationUser { get; set; }
    }
}
