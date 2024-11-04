using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopASP.Application.DTO;
using ShopASP.Application.Service;
using ShopASP.Domain.Entities;
using ShopASP.Domain.Interfaces;
using ShopASP.Infrastructure.Data;
using ShopASP.Web.Models;

namespace ShopASP.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly GenericInterface<UserDTO> db;
        private readonly GenericInterface<RoleDTO> db2;
        public UserController(GenericInterface<UserDTO> _db, GenericInterface<RoleDTO> _db2)
        {
            db = _db;
            db2 = _db2;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            var roles = await db2.GetAllAsync();
            var users = await db.GetAllAsync();

            foreach (var user in users)
            {
                var role = roles.FirstOrDefault(r => r.ID == user.RoleID);
                if(role != null)
                {
                    user.RoleName = role.Name;
                }
            }

            var viewModel = new UserRoleViewModel
            {
                Roles = roles,
                Users = users
            };
            return View(viewModel);
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await db.GetByIDAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: User/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.RoleList = new SelectList(await db2.GetAllAsync(), "ID", "Name");
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserDTO user)
        {
            if (ModelState.IsValid)
            {
                await db.CreateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.RoleList = new SelectList(await db2.GetAllAsync(), "ID", "ID", user.RoleID);
            return View(user);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await db.GetByIDAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var roles = await db.GetAllAsync();

            ViewData["RoleID"] = new SelectList(roles, "ID", "ID", user.RoleID);
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Surname,Pathronomic,Login,Password,RoleID")] UserDTO userdto)
        {
            if (id != userdto.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await db.UpdateAsync(userdto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (userdto.ID == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleID"] = new SelectList(userdto.RoleName, "ID", "ID", userdto.RoleID);
            return View(userdto);
        }

        public async Task<IActionResult> Verify(int id, [Bind("ID,Name,Surname,Pathronomic,Login,Password,RoleID")] UserDTO userdto)
        {
            if(id != userdto.ID)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    await db.VerifyAsync(userdto);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(userdto.ID == null)
                    {
                        return NotFound();
                    }
                }
            }
            return View(userdto);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await db.GetByIDAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await db.GetByIDAsync(id);
            if (user != null)
            {
                await db.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
