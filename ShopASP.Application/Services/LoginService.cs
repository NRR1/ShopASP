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

        public async Task<UserDTO> Register(UserDTO userDTO)
        {
            User user = _mapper.Map<User>(userDTO);
            User nUser = await _userRepository.Register(user);
            return _mapper.Map<UserDTO>(nUser);
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
