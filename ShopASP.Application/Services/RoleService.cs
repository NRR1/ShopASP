using AutoMapper;
using ShopASP.Application.DTO;
using ShopASP.Application.Interfaces;
using ShopASP.Domain.Entities;
using ShopASP.Domain.Interfaces;

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
            try
            {
                return mapper.Map<IEnumerable<RoleDTO>>(roles);
            }
            catch (Exception ex)
            {
                Console.WriteLine("service");
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
        public async Task<RoleDTO> GetByIDAsync(int id)
        {
            Role role = await roleRepository.GetByIDAsync(id);
            try
            {
                return mapper.Map<RoleDTO>(role);
            }
            catch (Exception ex)
            {
                Console.WriteLine("service");
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
        public async Task CreateAsync(RoleDTO entity)
        {
            Role role = mapper.Map<Role>(entity);
            try
            {
                await roleRepository.CreateAsync(role);
            }
            catch (Exception ex)
            {
                Console.WriteLine("service");
                Console.WriteLine(ex.Message.ToString());
            }
        }
        public async Task UpdateRole(RoleDTO roleDTO)
        {
            Role role = mapper.Map<Role>(roleDTO);
            try
            {
                await roleRepository.UpdateRole(role);
            }
            catch(Exception ex)
            {
                Console.WriteLine("service");
                Console.WriteLine(ex.Message.ToString());
            }
        }
        public async Task DeleteAsync(int id)
        {
            try
            {
                await roleRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("service");
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}
