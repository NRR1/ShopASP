using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopASP.Application.DTO;
using ShopASP.Application.Interface;
using ShopASP.Domain.Entities;
using ShopASP.Web.Models;

namespace ShopASP.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly UserManager<User> userManager;
        public OrderController(IOrderService _orderService, IProductService _productService, UserManager<User> _userManager)
        {
            orderService = _orderService;
            productService = _productService;
            userManager = _userManager;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<OrderDTO> orders = await orderService.GetAll();
            List<OrderListViewModel> vm = orders.Select(dto => OrderListViewModel.FromDTO(dto)).ToList();    
            return View(vm);
        }

        public async Task<IActionResult> Details(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var order = await orderService.GetByID(id);
            if(order == null)
            {
                return NotFound();
            }
            var vm = OrderViewModel.FromDTO(order);
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var products = await productService.GetAll();
            var productVM = products.Select(p => new ProductViewModel
            {
                ID = p.pdID,
                Name = p.pdName,
                Description = p.pdDescription,
                Cost = p.pdCost,
                Quantity = p.pdQuantity
            }).ToList();

            var vm = new CreateOrderViewModel
            {

            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,UserID,OrderDate,TotalAmount")] CreateOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Получаем пользователя, который создает заказ
                var user = await userManager.GetUserAsync(User);

                // Рассчитываем общую стоимость заказа
                decimal totalAmount = model.OrderProducts.Sum(p => p.Quantity * p.Product.Cost);

                // Создаем новый заказ
                var order = new Order
                {
                    UserId = user.Id,
                    OrderDate = DateTime.Now,
                    TotalAmount = totalAmount,
                    OrderProducts = model.OrderProducts.Select(op => new OrderProduct
                    {
                        ProductID = op.Product.ID,
                        Quantity = op.Quantity
                    }).ToList()
                };

                await service.Create(order);

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var order = await orderService.GetByID(id);
            if(order == null)
            {
                return NotFound();
            }
            OrderViewModel vm = OrderViewModel.FromDTO(order);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,UserID,OrderDate,TotalAmount")] OrderViewModel model)
        {
            if(id != model.OrderID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                OrderDTO dto = model.ToDTO();
                await orderService.Update(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var order = await orderService.GetByID(id);
            if(order == null)
            {
                return NotFound();
            }
            OrderViewModel model = OrderViewModel.FromDTO(order);
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConf(int id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var order = await orderService.GetByID(id);
            if(order == null)
            {
                return NotFound();
            }
            await orderService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
