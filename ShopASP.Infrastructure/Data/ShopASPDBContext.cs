using Microsoft.EntityFrameworkCore;
using ShopASP.Domain.Entities;

namespace ShopASP.Infrastructure.Data
{
    public class ShopASPDBContext : DbContext
    {
        public ShopASPDBContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
