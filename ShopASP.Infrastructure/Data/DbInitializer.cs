using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShopASP.Domain.Entities;

namespace ShopASP.Infrastructure.Data
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>(); // Используем кастомный User
            var logger = serviceProvider.GetRequiredService<ILogger<DbInitializer>>();

            var roles = new[] { "Admin", "Moderator", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var user = await userManager.FindByEmailAsync("admin@mail.ru");
            if (user == null)
            {
                user = new User
                {
                    UserName = "admin@mail.ru",
                    Email = "admin@mail.ru"
                };
                var result = await userManager.CreateAsync(user, "@dminPassword667");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    logger.LogInformation($"Пользователь {user.Email} успешно создан и назначена роль Admin");
                }
                else
                {
                    logger.LogError($"Не удалось создать пользователя admin@mail.ru. Ошибки: {string.Join(", ", result.Errors)}");
                }
            }
        }
    }
}
