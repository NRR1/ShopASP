using Microsoft.AspNetCore.Identity;

namespace ShopASP.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pathronomic { get; set; }
    }
}
