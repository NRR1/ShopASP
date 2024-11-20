using Microsoft.AspNetCore.Mvc;
using ShopASP.Application.DTO;
using ShopASP.Application.Interfaces;
using ShopASP.Application.Services;
using ShopASP.Domain.Interfaces;
using ShopASP.Infrastructure.Repositories;
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
            Console.WriteLine($"Retrieved {products.Count()} products from service.");

            var viewModel = new ProductViewModel
            {
                Products = products
            };
            Console.WriteLine($"[Controller] ViewModel contains {viewModel.Products.Count()} products.");
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
            //Не работает

            /*
             InvalidOperationException: The model item passed into the ViewDataDictionary is of type 'ShopASP.Web.Models.ProductViewModel', 
             but this ViewDataDictionary instance requires a model item of type 'ShopASP.Web.Models.DetailProductViewModel'.
             */
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

        //Delete не существует
        //Update не существует
    }
}
