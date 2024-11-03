using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopASP.Application.DTO;
using ShopASP.Domain.Entities;
using ShopASP.Domain.Interfaces;
using ShopASP.Infrastructure.Data;

namespace ShopASP.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly GenericInterface<UserDTO> db;
        public UserController(GenericInterface<UserDTO> _db)
        {
            db = _db;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            return View(await db.GetAllAsync());
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Surname,Pathronomic,Login,Password,RoleID")] UserDTO user)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(user);
                //await _context.SaveChangesAsync();
                await db.CreateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleID"] = new SelectList(user.Roles, "ID", "ID", user.RoleID);
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
            ViewData["RoleID"] = new SelectList(user.Roles, "ID", "ID", user.RoleID);
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
                    if (!UserExists(userdto.ID))
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
            ViewData["RoleID"] = new SelectList(userdto.Roles, "ID", "ID", userdto.RoleID);
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
