using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ShopASP.Infrastructure.Data
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var logger = serviceProvider.GetRequiredService<ILogger<DbInitializer>>();

            var roles = new[] { "Admin", "Moderator", "User" };
            foreach (var role in roles)
            {
                if(!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            //AvdoshkaMMM
            //@vdoshkaMMM931
            var user = await userManager.FindByEmailAsync("admin@mail.ru");
            if(user == null)
            {
                user = new IdentityUser
                {
                    UserName = "admin@mail.ru",
                    Email = "admin@mail.ru"
                };
                var result = await userManager.CreateAsync(user, "@dminPassword667");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    logger.LogInformation($"Пользователь {user.Email.ToString()} успешно создан и назначена роль Admin");
                }
                else
                {
                    logger.LogError($"Не удалось создать пользователя admin@mail.ru. Ошибки: {string.Join(", ", result.Errors)}");
                }
            }
        }
    }
}
