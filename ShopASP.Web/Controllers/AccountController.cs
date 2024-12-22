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
                var user = new User { UserName = regModel.Login, Email = regModel.Email };
                var result = await userManager.CreateAsync(user, regModel.Password);

                if (result.Succeeded)
                {
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
                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, logModel.Password, logModel.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        // Сохраняем роль в сессии
                        var role = await userManager.GetRolesAsync(user);
                        HttpContext.Session.SetString("UserRole", role.FirstOrDefault());

                        // Перенаправляем на URL, если он есть, иначе на главную страницу
                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    }
                }
            }
            return View(logModel);
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
