using Microsoft.AspNetCore.Identity;

namespace ShopASP.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Pathronomic { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
    }
}
