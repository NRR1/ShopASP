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

namespace ShopASP.Application.Service
{
    public class UserService : IUserService
    {
        private readonly GenericInterface<User> iuser;
        private readonly IMapper mapper;
        public UserService(GenericInterface<User> _iuser, IMapper _mapper)
        {
            iuser = _iuser;
            mapper = _mapper;
        }
        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            IEnumerable<User> users = await iuser.GetAllAsync();
            return mapper.Map<IEnumerable<UserDTO>>(users);
        }
        public async Task<UserDTO> GetByIDAsync(int id)
        {
            User user = await iuser.GetByIDAsync(id);
            return mapper.Map<UserDTO>(user);
        }
        public Task CreateAsync(UserDTO entity)
        {
            User user = mapper.Map<User>(entity);
            return iuser.CreateAsync(user);
        }
        public Task UpdateAsync(UserDTO entity)
        {
            User user = mapper.Map<User>(entity);
            return null;
            //return iuser.UpdateAsync(user);
        }
        public Task VerifyAsync(UserDTO entity)
        {
            User user = mapper.Map<User>(entity);
            return null;
            //return iuser.VerifyAsync(user);
        }
        public Task DeleteAsync(int id)
        {
            return iuser.DeleteAsync(id);
        }
    }
}
