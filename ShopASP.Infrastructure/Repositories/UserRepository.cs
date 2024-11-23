using Microsoft.EntityFrameworkCore;
using ShopASP.Domain.Entities;
using ShopASP.Domain.Interfaces;
using ShopASP.Infrastructure.Data;

namespace ShopASP.Infrastructure.Repositories
{
    public class UserRepository //: IUserRepository
    {
        //private readonly ShopASPDBContext db;
        //public UserRepository(ShopASPDBContext _db)
        //{
        //    db = _db;
        //}

        //public async Task<IEnumerable<User>> GetAllAsync()
        //{
        //    List<User> users = await db.Users.Include(r => r.Roles).ToListAsync();
        //    return users;
        //}
        //public async Task<User> GetByIDAsync(int id)
        //{
        //    var user = new User();//User user = await db.Users.Include(r => r.Roles).FirstOrDefaultAsync(x => x.ID == id);
        //    return user;
        //}
        //public async Task<User> Login(string login, string password)
        //{
        //    return await db.Users.FirstOrDefaultAsync(u => u.Login == login && u.Password == password);
        //}   
        //public async Task CreateAsync(User entity)
        //{
        //    if(entity == null)
        //    {
        //        Console.WriteLine("Nothing to add");
        //        return;
        //    }
        //    User user = new User
        //    {
        //        Name = entity.Name,
        //        Surname = entity.Surname,
        //        Pathronomic = entity.Pathronomic,
        //        Login = entity.Login,
        //        Password = entity.Password,
        //        RoleID = 2
        //    };
        //    await db.Users.AddAsync(user);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.Message.ToString());
        //    }
        //}
        //public async Task Update(User user)
        //{
        //    db.Entry(user).State = EntityState.Modified;
        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.Message.ToString());
        //    }
        //}
        //public async Task Verify(User user)
        //{
        //    if(user == null)
        //    {
        //        Console.WriteLine("Value is null");
        //    }
        //    User exUser = await db.Users.FindAsync(user.ID);
        //    if(exUser != null)
        //    {
        //        exUser.RoleID = 1;
        //        try
        //        {
        //            await db.SaveChangesAsync();
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message.ToString());
        //        }
        //    }
        //}
        //public async Task DeleteAsync(int id)
        //{
        //    User? user = await db.Users.FirstOrDefaultAsync(x => x.ID == id);
        //    db.Users.Remove(user);
        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.Message.ToString());
        //    }
        //}


    }
}
