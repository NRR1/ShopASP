using ShopASP.Application.DTO;
using ShopASP.Domain.Interfaces;

namespace ShopASP.Application.Interfaces
{
    public interface IUserService : GenericInterface<UserDTO>
    {
        Task UpdateUser(UserDTO userDTO);
        Task VerifyUser(UserDTO userDTO);
    }
}
