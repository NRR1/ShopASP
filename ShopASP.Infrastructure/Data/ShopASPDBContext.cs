using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopASP.Domain.Entities;

namespace ShopASP.Infrastructure.Data
{
    public class ShopASPDBContext : IdentityDbContext
    {
        public ShopASPDBContext(DbContextOptions<ShopASPDBContext> options) : base(options)
        {
            
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
    }
}