using Microsoft.EntityFrameworkCore;
using ShopASP.Domain.Entities;
using ShopASP.Domain.Interfaces;
using ShopASP.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP.Infrastructure.Repositories
{
    public class RoleRepository : IRole
    {
        private readonly ShopASPDBContext db;
        public RoleRepository(ShopASPDBContext _db)
        {
            db = _db;
        }
        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            List<Role> roles = await db.Roles.ToListAsync();
            try
            {
                return roles;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
        public async Task<Role> GetByIDAsync(int id)
        {
            Role role = await db.Roles.FindAsync(id);
            try
            {
                return role;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
        public async Task CreateAsync(Role entity)
        {
            await db.Roles.AddAsync(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
        public async Task UpdateRole(Role role)
        {
            db.Roles.Update(role);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
        public async Task DeleteAsync(int id)
        {
            Role role = await db.Roles.FindAsync(id);
            db.Roles.Remove(role);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}
