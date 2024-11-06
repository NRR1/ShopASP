using Microsoft.AspNetCore.Identity;

namespace ShopASP.Web.Models
{
    public class EditUserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public IList<string> UserRoles { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
    }
}
