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
    public class RoleController : Controller
    {
        private readonly GenericInterface<RoleDTO> db;

        public RoleController(GenericInterface<RoleDTO> _db)
        {
            db = _db;
        }

        // GET: Role
        public async Task<IActionResult> Index()
        {
            return View(await db.GetAllAsync());
        }

        // GET: Role/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await db.GetByIDAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: Role/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] RoleDTO roledto)
        {
            if (ModelState.IsValid)
            {
                await db.CreateAsync(roledto);
                return RedirectToAction(nameof(Index));
            }
            return View(roledto);
        }

        // GET: Role/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await db.GetByIDAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Role/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] RoleDTO roledto)
        {
            if (id != roledto.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    return null;
                    //await db.UpdateAsync(roledto);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (roledto.ID == null)
                    {
                        return NotFound(ex.Message);
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(roledto);
        }

        // GET: Role/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await db.GetByIDAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var role = await db.GetByIDAsync(id);
            if (role != null)
            {
                await db.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
