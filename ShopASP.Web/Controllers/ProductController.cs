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

            // Логирование в контроллере
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
            var product = await productService.GetByIDAsync(id); // Получаем продукт по ID
            if (product == null)
            {
                return NotFound(); // Если продукт не найден, возвращаем ошибку 404
            }

            var viewModel = new ProductViewModel
            {
                Product = product // Данные уже маппированы в сервисе
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
    }
}
