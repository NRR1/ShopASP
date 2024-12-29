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
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var logger = serviceProvider.GetRequiredService<ILogger<DbInitializer>>();

            // Создание ролей
            string[] roles = { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                    logger.LogInformation($"Роль {role} успешно создана");
                }
                else
                {
                    logger.LogInformation($"Ошибка где-то в роли");
                }
            }

            // Создание пользователя-администратора
            string adminEmail = "admin@mail.ru";
            string adminPassword = "@dminPASSWORD667";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = "Admin",
                    Email = adminEmail,
                    FirstName = "админ",
                    LastName = "админ",
                    Pathronomic = "админ",
                    EmailConfirmed = true // Подтверждение email сразу
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    // Назначаем роль "Admin"
                    var addToRoleResult = await userManager.AddToRoleAsync(adminUser, "Admin");
                    if (addToRoleResult.Succeeded)
                    {
                        logger.LogInformation($"Пользователь {adminEmail} успешно создан и назначена роль Admin.");
                    }
                    else
                    {
                        logger.LogError($"Не удалось назначить роль Admin пользователю {adminEmail}. Ошибки: {string.Join(", ", addToRoleResult.Errors)}");
                    }
                }
                else
                {
                    logger.LogError($"Не удалось создать пользователя {adminEmail}. Ошибки: {string.Join(", ", result.Errors)}");
                }
            }
            else
            {
                logger.LogInformation($"Пользователь с email {adminEmail} уже существует.");
            }
        }
    }
}