using Microsoft.AspNetCore.Mvc;
using ShopASP.Application.DTO;
using ShopASP.Application.Interfaces;
using ShopASP.Web.Models;

namespace ShopASP.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllAsync();
            
            var viewModel = new ProductListViewModel
            {
                Products = products
            };
            return View(viewModel);
        }


        public async Task<IActionResult> Details(int id)
        {
            var product = await productService.GetByIDAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new DetailProductViewModel
            {
                Name = product.pName,
                Description = product.pDescription,
                Price = product.pCost,
                Quantity = product.pQuantity
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            var view = new ProductViewModel
            {
                Product = new ProductDTO()
            };
            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                await productService.CreateAsync(model.Product);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var prod = await productService.GetByIDAsync(id);
            if (prod == null)
            {
                return NotFound();
            }

            var viewModel = new ProductViewModel
            {
                Product = prod
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await productService.UpdateProduct(model.Product);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var dprod = await productService.GetByIDAsync(id);
            if (dprod == null)
            {
                return NotFound();
            }

            var model = new DetailProductViewModel
            {
                Id = dprod.pID,
                Name = dprod.pName,
                Description = dprod.pDescription,
                Price = dprod.pCost,
                Quantity = dprod.pQuantity,
            };
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prod = await productService.GetByIDAsync(id);
            if(prod != null)
            {
                await productService.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
