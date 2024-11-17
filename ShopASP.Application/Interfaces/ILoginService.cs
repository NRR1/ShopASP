using ShopASP.Application.DTO;

namespace ShopASP.Application.Interfaces
{
    public interface ILoginService
    {
        Task<UserDTO> Login(string login, string password);
        Task<UserDTO> Register(UserDTO userDTO);
        Task<bool> Verify(int id);
        Task<bool> ResetPassword(int id, string nPassword);
    }
}
