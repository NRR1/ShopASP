using Microsoft.EntityFrameworkCore;
using ShopASP.Domain.Entities;
using ShopASP.Domain.Interfaces;
using ShopASP.Infrastructure.Data;

namespace ShopASP.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ShopASPDBContext db;
        public RoleRepository(ShopASPDBContext _db)
        {
            db = _db;
        }
        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            IEnumerable<Role> roles = await db.Roles.ToListAsync();
            return roles;
        }
        public async Task<Role> GetByIDAsync(int id)
        {
            Role? role = await db.Roles.FindAsync(id);
            return role;
        }
        public async Task CreateAsync(Role entity)
        {
            await db.Roles.AddAsync(entity);
            await db.SaveChangesAsync();
        }
        public async Task UpdateRole(Role role)
        {
            db.Roles.Update(role);
            await db.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Role? role = await db.Roles.FindAsync(id);
            db.Roles.Remove(role);
            await db.SaveChangesAsync();
        }
    }
}
