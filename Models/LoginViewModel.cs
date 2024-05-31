using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "The email is not the correct format")]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
