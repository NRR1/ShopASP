using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ShopASP.Application.DTO;
using ShopASP.Domain.Entities;
using ShopASP.Domain.Interfaces;
using ShopASP.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Application.Service
{
    public class RoleService : GenericInterface<RoleDTO>
    {
        private readonly GenericInterface<Role> irole;
        private readonly IMapper mapper;
        public RoleService(GenericInterface<Role> _irole, IMapper _mapper)
        {
            irole = _irole;
            mapper = _mapper;
        }
        public async Task<IEnumerable<RoleDTO>> GetAllAsync()
        {
            IEnumerable<Role> roles = await irole.GetAllAsync();
            return mapper.Map<IEnumerable<RoleDTO>>(roles); 
        }
        public async Task<RoleDTO> GetByIDAsync(int id)
        {
            Role role = await irole.GetByIDAsync(id);
            return mapper.Map<RoleDTO>(role);
        }
        public Task CreateAsync(RoleDTO entity)
        {
            Role role = mapper.Map<Role>(entity);
            return irole.CreateAsync(role);
        }
        public Task UpdateAsync(RoleDTO entity)
        {
            Role role = mapper.Map<Role>(entity);
            return irole.UpdateAsync(role);
        }
        public Task DeleteAsync(int id)
        {
            return irole.DeleteAsync(id);
        }
        public Task VerifyAsync(RoleDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
