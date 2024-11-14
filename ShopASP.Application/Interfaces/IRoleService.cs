using ShopASP.Application.DTO;
using ShopASP.Domain.Interfaces;

namespace ShopASP.Application.Interfaces
{
    public interface IRoleService : GenericInterface<RoleDTO>
    {
        Task UpdateRole(RoleDTO roleDTO);
    }
}
