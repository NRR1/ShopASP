using Microsoft.AspNetCore.Mvc;
using ShopASP.Application.DTO;
using ShopASP.Application.Interfaces;
using ShopASP.Domain.Interfaces;
using ShopASP.Infrastructure.Repositories;
using ShopASP.Web.Models;

namespace ShopASP.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductReposutory productService;
        public ProductController(IProductReposutory _productService)
        {
            productService = _productService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllAsync();
            var viewModel = new ProductViewModel
            {
                Products = products.Select(p => new ProductDTO
                {
                    pID = p.ID,
                    pName = p.Name,
                    pDescription = p.Description,
                    pCost = p.Cost,
                    pQuantity = p.Quantity
                }).ToList()
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await productService.GetByIDAsync(id);
            if(product == null)
            {
                return NotFound();
            }
            var viewModel = new DetailProductViewModel
            {
                Id = id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Cost,
                Quantity = product.Quantity
            };
            return View(viewModel);
        }
    }
}
