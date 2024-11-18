using AutoMapper;
using ShopASP.Application.DTO;
using ShopASP.Application.Interfaces;
using ShopASP.Domain.Entities;
using ShopASP.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<bool> Register(UserDTO dto)
        {
            try
            {
                // Создаем нового пользователя
                var user = new User
                {
                    Name = dto.uName,
                    Surname = dto.uSurname,
                    Pathronomic = dto.uPathronomic,
                    Login = dto.uLogin,
                    Password = dto.uPassword,
                    RoleID = 2 // Роль по умолчанию
                };

                // Пытаемся сохранить пользователя в базу данных
                await _userRepository.Register(user);

                // Возвращаем DTO при успешной регистрации
                return true;
            }
            catch (Exception ex)
            {
                // Ловим исключения, например, при нарушении уникальности
                if (ex.InnerException?.Message.Contains("UNIQUE") == true)
                {
                    // Если логин уже существует, возвращаем null
                    return false;
                }

                // Обрабатываем другие исключения или пробрасываем дальше
                throw;
            }
        }



        public async Task<bool> ResetPassword(int id, string nPassword)
        {
            bool result = await _userRepository.ReserPassword(id, nPassword);
            return result;
        }

        public async Task<bool> Verify(int id)
        {
            bool result = await _userRepository.Verify(id);
            return result;
        }
    }
}
