using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriEnergyConnect.Models
{
    public class ApplicationUser : IdentityUser
    {        

        [Required]
        [Display(Name = "Are you an employee?")]
        public bool Role { get; set; }


    }
}
