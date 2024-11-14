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
    public class RoleService : IRoleService
    {
        private readonly IMapper mapper;
        private readonly IRoleRepository roleRepository;

        public RoleService(IMapper _mapper, IRoleRepository _roleRepository)
        {
            mapper = _mapper;
            roleRepository = _roleRepository;
        }

        public async Task<IEnumerable<RoleDTO>> GetAllAsync()
        {
            IEnumerable<Role> roles = await roleRepository.GetAllAsync();
            return mapper.Map<IEnumerable<RoleDTO>>(roles);
        }
        public async Task<RoleDTO> GetByIDAsync(int id)
        {
            Role role = await roleRepository.GetByIDAsync(id);
            return mapper.Map<RoleDTO>(role);
        }
        public async Task CreateAsync(RoleDTO entity)
        {
            Role role = mapper.Map<Role>(entity);
            await roleRepository.CreateAsync(role);
        }
        public async Task UpdateRole(RoleDTO roleDTO)
        {
            Role role = mapper.Map<Role>(roleDTO);
            await roleRepository.UpdateRole(role);
        }
        public async Task DeleteAsync(int id)
        {
            await roleRepository.DeleteAsync(id);
        }
    }
}
