using AutoMapper;
using ShopASP.Application.DTO;
using ShopASP.Application.Interfaces;
using ShopASP.Domain.Entities;
using ShopASP.Domain.Interfaces;

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
            try
            {
                return mapper.Map<IEnumerable<UserDTO>>(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Service error : " + ex.Message.ToString());
                return null;
            }
        }
        public async Task<UserDTO> GetByIDAsync(int id)
        {
            User user = await userRepository.GetByIDAsync(id);
            try
            {
                return mapper.Map<UserDTO>(user);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Service error : " + ex.Message.ToString());
                return null;
            }
        }
        public Task CreateAsync(UserDTO entity)
        {
            User user = mapper.Map<User>(entity);
            try
            {
                return userRepository.CreateAsync(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Service error : "+ex.Message.ToString());
                return null;
            }
        }
        public Task UpdateUser(UserDTO userDTO)
        {
            User user = mapper.Map<User>(userDTO);
            try
            {
                return userRepository.Update(user);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Service error : " + ex.Message.ToString());
                return null;
            }
        }
        public Task VerifyUser(UserDTO userDTO)
        {
            User user = mapper.Map<User>(userDTO);
            try
            {
                return userRepository.Verify(user);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Service error : " + ex.Message.ToString());
                return null;
            }
        }
        public Task DeleteAsync(int id)
        {
            try
            {
                return userRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Service error : " + ex.Message.ToString());
                return null;
            }
        }
    }
}
