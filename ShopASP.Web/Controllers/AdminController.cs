using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopASP.Web.Models;
using System.Runtime.CompilerServices;

namespace ShopASP.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Users()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public async Task<IActionResult> Roles()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        public async Task<IActionResult> EditUserRole(string userID)
        {
            var user = await _userManager.FindByIdAsync(userID);
            if(user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.ToList();

            var model = new EditUserRoleViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                UserRoles = userRoles,
                AllRoles = allRoles
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserRole(string userId, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var rolesToAdd = roles.Except(userRoles);
            var rolesToRemove = userRoles.Except(roles);

            await _userManager.AddToRolesAsync(user, rolesToAdd);
            await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

            return RedirectToAction("Users");
        }
    }
}
