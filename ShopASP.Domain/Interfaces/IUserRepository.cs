using ShopASP.Domain.Entities;

namespace ShopASP.Domain.Interfaces
{
    public interface IUserRepository : GenericInterface<User>
    {
        Task<User> Login(string login, string password);
        Task Update(User user);
        Task Verify(User user);
    }
}
