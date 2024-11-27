using System.ComponentModel.DataAnnotations;

namespace ShopASP.Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        public string? Pathronomic { get; set; }

        [Required]
        [EmailAddress]
        public string Login { get; set; } // Используется как Email

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
