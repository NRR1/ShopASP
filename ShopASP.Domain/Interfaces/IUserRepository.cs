using ShopASP.Domain.Entities;

namespace ShopASP.Domain.Interfaces
{
    public interface IUserRepository : GenericInterface<User>
    {
        Task Update(User user);
        Task Verify(User user);
    }
}
