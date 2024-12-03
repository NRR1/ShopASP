using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopASP.Domain.Entities;
using ShopASP.Domain.Interfaces;

namespace ShopASP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        //private readonly GenericInterface<Role> db;
        //public RoleController(GenericInterface<Role> _db)
        //{
        //    db = _db;
        //}

        //[HttpGet("See all roles")]
        //public async Task<ActionResult<Role>> GetRoles()
        //{
        //    return Ok(await db.GetAllAsync());
        //}

        //[HttpGet("See role by ID")]
        //public async Task<IActionResult> GetRole(int id)
        //{
        //    return Ok(await db.GetByIDAsync(id));
        //}

        //[HttpPost("Create role")]
        //public async Task<ActionResult<Role>> CreateRole(Role role)
        //{
        //    await db.CreateAsync(role);
        //    return Ok();
        //}

        //[HttpPut("Update role")]
        //public async Task<ActionResult<Role>> UpdateRole(Role role)
        //{
        //    //await db.UpdateAsync(role);
        //    return Ok();
        //}
    }
}
