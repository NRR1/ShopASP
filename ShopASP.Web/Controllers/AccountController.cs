using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopASP.Domain.Entities;
using ShopASP.Web.Models;

namespace ShopASP.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> _userManager, SignInManager<User> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        public IActionResult SetRole()
        {
            HttpContext.Session.SetString("UserRole", "Admin");
            return RedirectToAction("Index", "Product");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel regModel)
        {
            if (ModelState.IsValid)
            {
                // Используем кастомный класс User
                var existingUserByLogin = await userManager.FindByNameAsync(regModel.Login);
                var existingUserByEmail = await userManager.FindByEmailAsync(regModel.Email);

                if (existingUserByLogin != null)
                {
                    ModelState.AddModelError(string.Empty, "Логин уже занят.");
                }
                if (existingUserByEmail != null)
                {
                    ModelState.AddModelError(string.Empty, "Email уже используется.");
                }
                if (existingUserByLogin == null && existingUserByEmail == null)
                {
                    User user = new User
                    {
                        UserName = regModel.Login,
                        Email = regModel.Email,
                        FirstName = regModel.Name,
                        LastName = regModel.Surname,
                        Pathronomic = regModel.Pathronomic
                    };
                    var result = await userManager.CreateAsync(user, regModel.Password);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Guest");
                        HttpContext.Session.SetString("UserID", user.Id);
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(regModel);
        }

        public IActionResult Login()
        {
            var lVM = new LoginViewModel();
            return View(lVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel logModel, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Используем кастомный класс User
                var user = await userManager.FindByEmailAsync(logModel.Login);
                if (user == null)
                {
                    user = await userManager.FindByNameAsync(logModel.Login);// == username == login
                }
                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, logModel.Password, logModel.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        HttpContext.Session.SetString("UserID", user.Id);

                        var role = await userManager.GetRolesAsync(user);
                        HttpContext.Session.SetString("UserRole", role.FirstOrDefault());
                        await SignInAsync(user);

                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    }
                }
            }
            return View(logModel);
        }
        private async Task SignInAsync(User user)
        {
            await signInManager.SignInAsync(user, isPersistent: false);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Product"); // Перенаправление на страницу продуктов
            }
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Product");
        }
    }
}
