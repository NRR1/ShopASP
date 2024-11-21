using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            //Console.WriteLine($"Retrieved {products.Count()} products from service.");

            var viewModel = new ProductListViewModel
            {
                Products = products
            };
            //Console.WriteLine($"[Controller] ViewModel contains {viewModel.Product} products.");
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
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}

            //// Обновляем продукт через сервис
            //var productDTO = model.Product; // Это ваш изменённый продукт
            //await productService.UpdateProduct(productDTO);

            //return RedirectToAction(nameof(Index));
            //if(id == null)
            //{
            //    return NotFound();
            //}
            //if(ModelState.IsValid)
            //{
            //    var prDTO = model.Product;
            //    try
            //    {
            //        await productService.UpdateProduct(prDTO);
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if(prDTO.pID == null)
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(model);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await productService.UpdateProduct(model.Product);

            return RedirectToAction(nameof(Index));
        }




        //Delete не существует
    }
}
