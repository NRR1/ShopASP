using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ShopASP.Application.DTO;
using ShopASP.Application.Interface;
using ShopASP.Web.Models;

namespace ShopASP.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService service;
        public ProductController(IProductService _service)
        {
            service = _service;
        }
        public async Task<IActionResult> Index()
        {
            var products = await service.GetAll();
            var vm = products.Select(dto => ProductListViewModel.FromDTO(dto)).ToList();
            return View(vm);
        }

        public async Task<IActionResult> Details(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            ProductDTO product = await service.GetByID(id);
            if(product == null)
            {
                return NotFound();
            }
            ProductViewModel vm = ProductViewModel.FromDTO(product);
            return View(vm);
        }

        [HttpGet]
        public IActionResult Create() => View(new ProductViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,Cost,Quantity")] ProductViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }
            ProductDTO product = vm.ToDTO();
            await service.Create(product);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var product = await service.GetByID(id);
            if(product == null)
            {
                return NotFound();
            }
            var vm = ProductViewModel.FromDTO(product);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Desctiption,Cost,Quantity")] ProductViewModel vm)
        {
            if(id != vm.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                ProductDTO dto = vm.ToDTO();
                await service.Update(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }


        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var product = await service.GetByID(id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost("Delete/{id}")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConf(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var dProd = await service.GetByID(id);
            if(dProd == null)
            {
                return NotFound();
            }
            await service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
} 
