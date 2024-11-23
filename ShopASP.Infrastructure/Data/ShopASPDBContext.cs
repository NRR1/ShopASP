using Microsoft.EntityFrameworkCore;
using ShopASP.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ShopASP.Infrastructure.Data
{
    public class ShopASPDBContext : DbContext//: IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ShopASPDBContext(DbContextOptions<ShopASPDBContext> options) : base(options)
        {
            
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
