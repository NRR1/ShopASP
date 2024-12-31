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
                User? existingUserByLogin = await userManager.FindByNameAsync(regModel.Login);
                User? existingUserByEmail = await userManager.FindByEmailAsync(regModel.Email);

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
                    IdentityResult? result = await userManager.CreateAsync(user, regModel.Password);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "User");
                        HttpContext.Session.SetString("UserID", user.Id);
                        await signInManager.SignInAsync(user, isPersistent: false);
                    }
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                return RedirectToAction("Index", "Product");
            }
            return View(regModel);
        }

        public IActionResult Login()
        {
            LoginViewModel lVM = new LoginViewModel();
            return View(lVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel logModel, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Используем кастомный класс User
                User? user = await userManager.FindByEmailAsync(logModel.Login);
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

        [HttpGet]
        public IActionResult ResetPassword()
        {
            if (TempData["ResetToken"] == null || TempData["Username"] == null)
            {
                return RedirectToAction("ForgotPassword");
            }

            ViewBag.Username = TempData["Username"];
            ViewBag.Token = TempData["ResetToken"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel viewModel)
        {
            //if(!ModelState.IsValid)
            //{
            //    return View(viewModel);
            //}

            //// Найти пользователя по имени пользователя
            //var user = await userManager.FindByNameAsync(viewModel.Login);
            //if (user == null)
            //{
            //    // Не раскрывать, существует ли пользователь
            //    return RedirectToAction("ForgotPasswordConfirmation");
            //}


            //// Генерация токена для сброса пароля
            //var token = await userManager.GeneratePasswordResetTokenAsync(user);

            //// Храните токен где-то (например, временно в TempData или базе данных)
            //TempData["ResetToken"] = token;
            //TempData["Username"] = viewModel.Login;

            //return RedirectToAction("ResetPassword");

            if(!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await userManager.FindByEmailAsync(viewModel.Login);
            if(user == null)
            {
                user = await userManager.FindByNameAsync(viewModel.Login);
                if(user == null)
                {
                    return RedirectToAction("ForgotPasswordConfirmation");
                }
            }
            if (user != null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                TempData["ResetToken"] = token;
                TempData["Username"] = viewModel.Login;
            }
            return RedirectToAction("ResetPassword");
        }



        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Product");
        }
    }
}
