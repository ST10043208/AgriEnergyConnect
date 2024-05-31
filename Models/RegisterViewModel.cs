using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "The email is not the correct format")]
        public required string Email { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public bool Role { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
