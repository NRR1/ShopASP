using System.ComponentModel.DataAnnotations;

namespace ShopASP.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter password")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
