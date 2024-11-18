using Microsoft.EntityFrameworkCore;
using ShopASP.Domain.Entities;
using ShopASP.Domain.Interfaces;
using ShopASP.Infrastructure.Data;

namespace ShopASP.Infrastructure.Repositories
{
    public class LoginRepository : ILogin<User>
    {
        private readonly ShopASPDBContext db;
        public LoginRepository(ShopASPDBContext _db)
        {
            db = _db;
        }

        public async Task<User> Login(string login, string password)
        {
            User user = await db.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Login == login && u.Password == password);
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

        public async Task<User> Register(User user)
        {
            try
            {
                User chechUser = await db.Users.FirstOrDefaultAsync(u => u.Login == user.Login);
                if (chechUser != null)
                {
                    Console.WriteLine("User is exist");
                    return null;
                }
                User nUser = new User
                {
                    Name = user.Name,
                    Surname = user.Surname,
                    Pathronomic = user.Pathronomic,
                    Login = user.Login,
                    Password = user.Password,
                    RoleID = 2
                };
                await db.Users.AddAsync(nUser);
                await db.SaveChangesAsync();
                return nUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }

        public async Task<bool> ReserPassword(string login, string nPassword)
        {
            try
            {
                User user = await db.Users.FirstOrDefaultAsync(x => x.Login == login);
                if (user == null)
                {
                    Console.WriteLine("User is null");
                    return false;
                }
                user.Password = nPassword;
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }

        public async Task<bool> Verify(int id)
        {
            try
            {
                User user = await db.Users.FirstOrDefaultAsync(x => x.ID == id);
                if (user == null)
                {
                    Console.WriteLine("User is null");
                    return false;
                }
                if (user.RoleID == 1)
                {
                    Console.WriteLine("User is verified");
                    return true;
                }
                user.RoleID = 1;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}