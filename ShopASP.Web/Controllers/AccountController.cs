using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopASP.Web.Models;

namespace ShopASP.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public AccountController(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _singInManager)
        {
            userManager = _userManager;
            signInManager = _singInManager;
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
                var user = new IdentityUser { UserName = regModel.Name, Email = regModel.Email };
                var result = await userManager.CreateAsync(user, regModel.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
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
        public async Task<IActionResult> Login(LoginViewModel logModel)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(logModel.Login, logModel.Password, logModel.RememberMe, lockoutOnFailure: false);
                if(result.Succeeded)
                {
                    Console.WriteLine("Nice login");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    Console.WriteLine("not nice");
                    ModelState.AddModelError(string.Empty, "Invalid login attemp");
                }
            }
            return View(logModel);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
