using ShopASP.Application.DTO;

namespace ShopASP.Web.Models
{
    public class UserRoleViewModel
    {
        public IEnumerable<RoleDTO> Roles { get; set; }
        public IEnumerable<UserDTO> Users { get; set; }
    }
}
