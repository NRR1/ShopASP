using Microsoft.AspNetCore.Mvc;
using ShopASP.Application.Interfaces;
using ShopASP.Domain.Interfaces;
using ShopASP.Web.Models;

namespace ShopASP.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        public ProductController(IProductService _product)
        {
            productService = _product;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllAsync();
            var viewModel = new ProductViewModel
            {
                Products = products
            };
            return View(viewModel);
            //products - объект, передающийся с сервиса
            //Products - объект, передающийся с ViewModel
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var product = await productService.GetByIDAsync(id);
            if(product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductViewModel
            {
                ID = product.pID,
                Name = product.pName,
                Description = product.pDescription,
                Cost = product.pCost,
                Quantity = product.pQuantity,
                Orders = product.pOrders
            };

            return View(viewModel);
        }


    }
}
