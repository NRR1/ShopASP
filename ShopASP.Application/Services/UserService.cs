using AutoMapper;
using ShopASP.Application.DTO;
using ShopASP.Application.Interfaces;
using ShopASP.Domain.Entities;
using ShopASP.Domain.Interfaces;
using ShopASP.Infrastructure.Repositories;

namespace ShopASP.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository _userRepository, IMapper _mapper)
        {
            userRepository = _userRepository;
            mapper = _mapper;
        }
        
        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            IEnumerable<User> users = await userRepository.GetAllAsync();
            return mapper.Map<IEnumerable<UserDTO>>(users);
        }
        public async Task<UserDTO> GetByIDAsync(int id)
        {
            User user = await userRepository.GetByIDAsync(id);
            return mapper.Map<UserDTO>(user);
        }
        public Task CreateAsync(UserDTO entity)
        {
            User user = mapper.Map<User>(entity);
            return userRepository.CreateAsync(user);
        }
        public Task UpdateUser(UserDTO userDTO)
        {
            User user = mapper.Map<User>(userDTO);
            return userRepository.Update(user);
        }
        public Task VerifyUser(UserDTO userDTO)
        {
            User user = mapper.Map<User>(userDTO);
            return userRepository.Verify(user);
        }
        public Task DeleteAsync(int id)
        {
            return userRepository.DeleteAsync(id);
        }
    }
}
