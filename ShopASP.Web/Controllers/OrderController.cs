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
            var products = await productService.GetAll();
            ProductViewModel VMproduct = new ProductViewModel();
            IEnumerable<OrderViewModel> model = orders.Select(order => new OrderViewModel
            {
                Id = order.dOrderID,
                UserID = order.dUserID,
                OrderDate = order.dOrderDate,
                TotalAmount = order.dTotalAmount,
                ProductName = VMproduct.Name,
                ProductCost = VMproduct.Cost
            });
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            OrderDTO? order = await orderService.GetByID(id);
            if(order == null)
            {
                return NotFound();
            }
            OrderViewModel model = new OrderViewModel
            {
                Id = order.dOrderID,
                UserID = order.dUserID,
                OrderDate = order.dOrderDate,
                TotalAmount = order.dTotalAmount
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create() => View(new OrderViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,UserID,OrderDate,TotalAmount")] OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            User? user = await userManager.GetUserAsync(User);
            OrderDTO order = new OrderDTO()
            {
                dOrderID = model.Id,
                dUserID = user.Id,
                dOrderDate = model.OrderDate,
                dTotalAmount = model.TotalAmount
            };
            await orderService.Create(order);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            OrderDTO order = await orderService.GetByID(id);
            User? user = await userManager.GetUserAsync(User);
            if(order == null)
            {
                return NotFound();
            }
            OrderViewModel vm = new OrderViewModel
            {
                Id = order.dOrderID,
                UserID = user.Id,
                OrderDate = order.dOrderDate,
                TotalAmount = order.dTotalAmount
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,UserID,OrderDate,TotalAmount")] OrderViewModel model)
        {
            if(id != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                User? user = await userManager.GetUserAsync(User);
                OrderDTO order = new OrderDTO
                {
                    dOrderID = model.Id,
                    dUserID = user.Id,
                    dOrderDate = model.OrderDate,
                    dTotalAmount = model.TotalAmount
                };
                await orderService.Update(order);
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
            OrderDTO order = await orderService.GetByID(id);
            User? user = await userManager.GetUserAsync(User);
            if(order == null)
            {
                return NotFound();
            }
            OrderViewModel vm = new OrderViewModel
            {
                Id = order.dOrderID,
                UserID = user.Id,
                OrderDate = order.dOrderDate,
                TotalAmount = order.dTotalAmount
            };
            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConf(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            OrderDTO order = await orderService.GetByID(id);
            if(order == null)
            {
                return NotFound();
            }
            await orderService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}