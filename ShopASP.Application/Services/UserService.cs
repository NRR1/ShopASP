using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
    public class UserService : IUserService
    {
        private readonly IUserRepository ur;
        private readonly IMapper mapper;

        public UserService(IUserRepository _ur, IMapper _mapper)
        {
            ur = _ur;
            mapper = _mapper;
        }


        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            IEnumerable<User> users = await ur.GetAllAsync();
            return mapper.Map<IEnumerable<UserDTO>>(users);
        }
        public async Task<UserDTO> GetByIDAsync(int id)
        {
            User user = await ur.GetByIDAsync(id);
            return mapper.Map<UserDTO>(user);
        }
        public Task CreateAsync(UserDTO entity)
        {
            User user = mapper.Map<User>(entity);
            return ur.CreateAsync(user);
        }
        public Task UpdateUser(UserDTO userDTO)
        {
            User user = mapper.Map<User>(userDTO);
            return ur.Update(user);
        }
        public Task VerifyUser(UserDTO userDTO)
        {
            User user = mapper.Map<User>(userDTO);
            return ur.Verify(user);
        }
        public Task DeleteAsync(int id)
        {
            return ur.DeleteAsync(id);
        }
    }
}
