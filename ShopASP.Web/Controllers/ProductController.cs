using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            IEnumerable<ProductDTO> products = await service.GetAll();
            List<ProductListViewModel> vm = products.Select(dto => ProductListViewModel.FromDTO(dto)).ToList();
            return View(vm);
        }


        [HttpGet]
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create() => View(new ProductViewModel());

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,Cost,Quantity")] ProductViewModel vm)
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
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

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConf(int id)
        {
            ProductDTO product = await service.GetByID(id);
            if(product != null)
            {
                await service.Delete(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 
