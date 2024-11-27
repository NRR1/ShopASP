using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopASP.Domain.Entities;

namespace ShopASP.Infrastructure.Data
{
    public class ShopASPDBContext : IdentityDbContext<User, Role, int>
    {
        public ShopASPDBContext(DbContextOptions<ShopASPDBContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Настройка таблицы Order
            builder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserID)
                .OnDelete(DeleteBehavior.Restrict); // Убираем каскадное удаление, чтобы избежать циклов

            builder.Entity<Order>()
                .HasOne(o => o.Product)
                .WithMany(p => p.Orders)
                .HasForeignKey(o => o.ProductID);

            // Настройка таблиц Identity
            builder.Entity<User>()
                .ToTable("AspNetUsers");

            builder.Entity<Role>()
                .ToTable("AspNetRoles");

            builder.Entity<IdentityUserRole<int>>()
                .ToTable("AspNetUserRoles")
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.Entity<IdentityUserLogin<int>>()
                .ToTable("AspNetUserLogins")
                .HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });

            builder.Entity<IdentityUserToken<int>>()
                .ToTable("AspNetUserTokens")
                .HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });

            builder.Entity<IdentityRoleClaim<int>>()
                .ToTable("AspNetRoleClaims");

            builder.Entity<IdentityUserClaim<int>>()
                .ToTable("AspNetUserClaims");
        }

    }
}
