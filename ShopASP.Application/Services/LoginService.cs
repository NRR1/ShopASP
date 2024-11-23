using AutoMapper;
using ShopASP.Application.DTO;
using ShopASP.Application.Interfaces;
using ShopASP.Domain.Entities;
using ShopASP.Domain.Interfaces;

namespace ShopASP.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IMapper _mapper;
        private readonly ILogin<User> _userRepository;
        public LoginService(IMapper mapper, ILogin<User> userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Login(string login, string password)
        {
            User user = await _userRepository.Login(login, password);
            try
            {
                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine("service error : " + ex.Message.ToString());
                return null;
            }
        }

        public async Task<bool> Register(UserDTO dto)
        {
            try
            {
                var user = new User
                {
                    Name = dto.uName,
                    Surname = dto.uSurname,
                    Pathronomic = dto.uPathronomic,
                    Login = dto.uLogin,
                    Password = dto.uPassword,
                };
                await _userRepository.Register(user);
                return true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException?.Message.Contains("UNIQUE") == true)
                {
                    Console.WriteLine("service error : " + ex.Message.ToString());
                    return false;
                }
                throw;
            }
        }



        public async Task<bool> ResetPassword(string login, string nPassword)
        {
            bool result = await _userRepository.ReserPassword(login, nPassword);
            try
            {
                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine("service error : " + ex.Message.ToString());
                return false;
            }
        }

        public async Task<bool> Verify(int id)
        {
            bool result = await _userRepository.Verify(id);
            try
            {
                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine("service error " + ex.Message.ToString());
                return false;
            }
        }
    }
}
