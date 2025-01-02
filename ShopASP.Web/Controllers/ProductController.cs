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


        // Эта часть не работает


        //[HttpGet]
        //public async Task<IActionResult> CreateOrder()
        //{
        //    // Получаем информацию о продукте по ID
        //    ProductDTO product = await productservice.GetByID(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    // Создаем модель для представления
        //    var orderProductViewModel = new OrderProductViewModel
        //    {
        //        ProductID = product.pdID,
        //        ProductName = product.pdName,
        //        ProductCost = product.pdCost,
        //        Quantity = 1 // Минимальное количество
        //    };

        //    return View(orderProductViewModel);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateOrder(OrderProductViewModel OPVM)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(OPVM); // Повторно показать форму
        //    }

        //    // Создание нового заказа
        //    var newOrder = new OrderDTO
        //    {
        //        dUserID = int.Parse(User.Identity.Name), // ID текущего пользователя
        //        dOrderDate = DateTime.Now,
        //        dTotalAmount = OPVM.ProductCost * OPVM.Quantity,
        //    };

        //    // Добавляем продукт в заказ
        //    newOrder.dOrderProducts.Add(new OrderProduct
        //    {
        //        ProductID = OPVM.ProductID,
        //        Quantity = OPVM.Quantity
        //    });

        //    // Сохранение заказа
        //    await orderservice.Create(newOrder);

        //    return RedirectToAction("Index", "Product"); // Перенаправление на список продуктов
        //}

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
