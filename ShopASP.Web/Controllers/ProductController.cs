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

        [HttpGet]
        public async Task<IActionResult> CreateOrder(int id)
        {
            // Получаем информацию о продукте по ID
            ProductDTO product = await productservice.GetByID(id);
            if (product == null)
            {
                return NotFound();
            }

            // Создаем модель для представления
            var orderProductViewModel = new OrderProductViewModel
            {
                ProductID = product.pdID,
                ProductName = product.pdName,
                ProductCost = product.pdCost,
                Quantity = 1 // Минимальное количество
            };

            return View(orderProductViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrder(OrderProductViewModel OPVM)
        {
            if (!ModelState.IsValid)
            {
                return View(OPVM); // Повторно показать форму, если есть ошибки
            }

            // Получаем информацию о продукте
            var product = await productservice.GetByID(OPVM.ProductID);
            if (product == null)
            {
                ModelState.AddModelError("", "Продукт не найден.");
                return View(OPVM);
            }

            // Проверяем наличие товара на складе
            if (product.pdQuantity < OPVM.Quantity)
            {
                ModelState.AddModelError("", "Недостаточно товара на складе.");
                return View(OPVM);
            }

            // Получаем ID пользователя из клейма
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized();
            }

            // 1. Создание нового заказа (связанного с пользователем)
            var newOrder = new OrderDTO
            {
                dUserID = userId.ToString(),
                dOrderDate = DateTime.Now,
                dTotalAmount = (decimal)(OPVM.ProductCost * OPVM.Quantity), // Приведение к decimal для расчета суммы
                dOrderProducts = new List<OrderProductDTO>()
            };

            // Добавляем продукт в заказ
            var orderProduct = new OrderProductDTO
            {
                dProductId = OPVM.ProductID,
                dQuantity = OPVM.Quantity
            };
            newOrder.dOrderProducts.Add(orderProduct);

            // 2. Сохраняем заказ в базе
            await orderservice.Create(newOrder); // сохраняем заказ

            // 3. Уменьшаем количество товара на складе
            product.pdQuantity -= OPVM.Quantity;
            await productservice.Update(product); // обновление товара в базе

            // 4. Добавляем запись в таблицу OrderProduct
            foreach (var orderProductDto in newOrder.dOrderProducts)
            {
                var orderProductEntity = new OrderProduct
                {
                    OrderID = newOrder.dOrderID,  // ID заказа (который генерируется после сохранения)
                    ProductID = orderProductDto.dProductId,
                    Quantity = orderProductDto.dQuantity
                };

                // Добавляем связь между заказом и продуктом в базу данных
                await orderservice.CreateOrderProduct(orderProductEntity); // Метод для добавления записи в таблицу OrderProduct
            }

            // Перенаправляем пользователя на список продуктов
            return RedirectToAction("Index", "Product");
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
