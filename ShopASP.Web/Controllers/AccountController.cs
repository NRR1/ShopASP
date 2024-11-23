using Microsoft.AspNetCore.Mvc;
using ShopASP.Domain.Entities;
using ShopASP.Domain.Interfaces;
using ShopASP.Web.Models;

namespace ShopASP.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogin<User> login;
        public AccountController(ILogin<User> _login)
        {
            login = _login;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, ErrorViewModel error)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await login.Login(model.UserName, model.Password);

            //if (user == null)
            //{
            //    ModelState.AddModelError("", "Пользователь не найден/Неправильный логин или пароль");
            //    return View(model);
            //}

            if (string.IsNullOrEmpty(model.UserName))
            {
                ModelState.AddModelError("", "Пользователь не найден");
                return View(model);
            }

            if (string.IsNullOrEmpty(model.Password))
            {
                ModelState.AddModelError("", "Неверный пароль");
                return View(model);
            }

            if (user != null)
            {
                return RedirectToAction("Index", "Product");
                //
            }

            ModelState.AddModelError("", "Неизвестная ошибка");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            User user = new User
            {
                Name = model.Name,
                Surname = model.Surname,
                Pathronomic = model.Pathronomic,
                Login = model.Login,
                Password = model.Password
            };
            var result = await login.Register(user);
            /*if(result == null)
            {
                ModelState.AddModelError("", "User is null");
                return View(model);
            }
            */
            return RedirectToAction("Index", "Product");
        }


        public async Task<IActionResult> ResetPassword(ReserPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var done = await login.ReserPassword(model.UserName, model.NewPassword);
            if (!done)
            {
                ModelState.AddModelError("", "Пользователь не найден.");
                return View(model);
            }
            return RedirectToAction("Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
