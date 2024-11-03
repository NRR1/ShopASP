using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopASP.Application.DTO;
using ShopASP.Domain.Entities;
using ShopASP.Domain.Interface;
using ShopASP.Domain.Interfaces;
using ShopASP.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Application.Service
{
    public class RoleService : IRoleService
    {
        private readonly GenericInterface<Role> irole;
        private readonly IMapper mapper;
        public RoleService(GenericInterface<Role> _irole, IMapper _mapper)
        {
            irole = _irole;
            mapper = _mapper;
        }
        public async Task<IEnumerable<RoleDTO>> GetRoles()
        {
            IEnumerable<Role> roles = await irole.GetAllAsync();
            return mapper.Map<IEnumerable<RoleDTO>>(roles);
        }
        public async Task<RoleDTO> GetRoleByID(int id)
        {
            Role role = await irole.GetByIDAsync(id);
            return mapper.Map<RoleDTO>(role);
        }
        public Task CreateRole(RoleDTO roledto)
        {
            Role role = mapper.Map<Role>(roledto);
            return irole.CreateAsync(role);
        }
        public Task UpdateRole(RoleDTO roledto)
        {
            Role role = mapper.Map<Role>(roledto);
            return irole.UpdateAsync(role);
        }
        public Task DeleteRole(int id)
        {
            return irole.DeleteAsync(id);
        }
    }
}
