using ShopASP.Domain.Entities;

namespace ShopASP.Domain.Interfaces
{
    public interface IRoleRepository : GenericInterface<Role>
    {
        Task UpdateRole(Role role);
    }
}
