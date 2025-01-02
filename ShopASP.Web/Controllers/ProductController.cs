using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopASP.Application.DTO;
using ShopASP.Application.Interface;
using ShopASP.Domain.Entities;
using ShopASP.Web.Models;

namespace ShopASP.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productservice;
        private readonly IOrderService orderservice;
        public ProductController(IProductService _productservice, IOrderService _orderservice)
        {
            productservice = _productservice;
            orderservice = _orderservice;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            IEnumerable<ProductDTO> products = await productservice.GetAll();
            IEnumerable<ProductViewModel> vm = products.Select(product => new ProductViewModel
            {
                ID = product.pdID,
                Name = product.pdName,
                Description = product.pdDescription,
                Cost = product.pdCost,
                Quantity = product.pdQuantity,
            }).ToList();
            return View(vm);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            ProductDTO product = await productservice.GetByID(id);
            if(product == null)
            {
                return NotFound();
            }
            ProductViewModel vm = new ProductViewModel
            {
                ID = product.pdID,
                Name = product.pdName,
                Description = product.pdDescription,
                Cost = product.pdCost,
                Quantity = product.pdQuantity,
            };
            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateProduct() => View(new ProductViewModel());

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct([Bind("ID,Name,Description,Cost,Quantity")] ProductViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }
            ProductDTO product = new ProductDTO()
            {
                pdID = vm.ID,
                pdName = vm.Name,
                pdDescription = vm.Description,
                pdCost = vm.Cost,
                pdQuantity = vm.Quantity,
            };
            await productservice.Create(product);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Guest")]
        [HttpGet]
        public async Task<IActionResult> CreateOrder()
        {
            var orderViewModel = new OrderViewModel
            {
                OrderDate = DateTime.Now,
                Products = new List<OrderProductViewModel>()
            };

            // Получаем все доступные продукты для выбора
            IEnumerable<ProductDTO> products = await productservice.GetAll();
            foreach (var product in products)
            {
                orderViewModel.Products.Add(new OrderProductViewModel
                {
                    ProductID = product.pdID,
                    ProductName = product.pdName,
                    ProductCost = product.pdCost,
                    Quantity = 1  // Поставим минимальное количество по умолчанию
                });
            }

            return View(orderViewModel);
        }

        [Authorize(Roles = "Guest")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrder(OrderViewModel orderModel)
        {
            if (!ModelState.IsValid)
            {
                return View(orderModel);
            }

            // Создаем заказ на основе переданных данных
            var newOrder = new OrderDTO
            {
                dUserID = orderModel.UserID,
                dOrderDate = orderModel.OrderDate,
                dTotalAmount = orderModel.TotalAmount,
            };

            // Добавляем продукты в заказ
            foreach (var product in orderModel.Products)
            {
                newOrder.dOrderProducts.Add(new OrderProductDTO
                {
                    dProductId = product.ProductID,
                    dQuantity = product.Quantity
                });
            }

            // Создаем заказ с помощью сервиса
            await orderservice.Create(newOrder);

            return RedirectToAction("OrderConfirmation");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            ProductDTO product = await productservice.GetByID(id);
            if(product == null)
            {
                return NotFound();
            }
            ProductViewModel vm = new ProductViewModel
            {
                ID = product.pdID,
                Name = product.pdName,
                Description = product.pdDescription,
                Cost = product.pdCost,
                Quantity = product.pdQuantity
            };
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
                ProductDTO dto = new ProductDTO
                {
                    pdID = vm.ID,
                    pdName = vm.Name,
                    pdDescription = vm.Description,
                    pdCost = vm.Cost,
                    pdQuantity = vm.Quantity
                };
                await productservice.Update(dto);
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
            ProductDTO product = await productservice.GetByID(id);
            if(product == null)
            {
                return NotFound();
            }
            ProductViewModel vm = new ProductViewModel
            {
                ID = product.pdID,
                Name = product.pdName,
                Description = product.pdDescription,
                Cost = product.pdCost,
                Quantity = product.pdQuantity
            };
            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConf(int id)
        {
            ProductDTO product = await productservice.GetByID(id);
            if(product != null)
            {
                await productservice.Delete(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 
