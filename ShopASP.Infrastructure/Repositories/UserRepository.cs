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
    public class UserRepository : GenericInterface<User>
    {
        private readonly ShopASPDBContext db;
        public UserRepository(ShopASPDBContext _db)
        {
            db = _db;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            List<User> users = await db.Users.Include(x => x.Roles).ToListAsync();
            try
            {
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
        public async Task<User> GetByIDAsync(int id)
        {
            User user = await db.Users.Include(x => x.Roles).FirstOrDefaultAsync(x => x.ID == id);
            try
            {
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
        public async Task CreateAsync(User entity)
        {
            entity.RoleID = 2; // set role guest for new user
            await db.Users.AddAsync(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
        public async Task<User> UpdateAsync(User entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return entity;
        }
        public async Task VerifyAsync(User entity)
        {
            if(entity == null)
            {
                Console.WriteLine("null");
                return;
            }
            User exUser = await db.Users.FindAsync(entity.ID);
            if (exUser != null)
            {
                exUser.RoleID = 1;
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
        public async Task DeleteAsync(int id)
        {
            User? user = await db.Users.Include(u => u.RoleID).FirstOrDefaultAsync(x => x.ID == id);
            try
            {
                db.Users.Remove(user);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}
