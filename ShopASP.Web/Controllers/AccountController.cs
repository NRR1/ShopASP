using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopASP.Application.DTO;
using ShopASP.Application.Interfaces;
using ShopASP.Domain.Entities;
using ShopASP.Domain.Interfaces;
using ShopASP.Web.Models;
using System.Net;
using System.Security.Claims;

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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var user = login.Login(model.UserName, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "error login or password");
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var newUser = new User
            {
                Name = model.Name,
                Surname = model.Surname,
                Pathronomic = model.Pathronomic,
                Login = model.Login,
                Password = model.Password
            };
            var result = await login.Register(newUser);
            if(result == null)
            {
                ModelState.AddModelError("", "User is not null");
                return View(model);
            }
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> ResetPassword(ReserPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var done = await login.ReserPassword(model.UserID, model.NewPassword);
            if (!done)
            {
                ModelState.AddModelError("", "Пользователь не найден.");
                return View(model);
            }
            return RedirectToAction("Login");
        }

    }
}
