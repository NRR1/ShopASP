using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopASP.Domain.Entities;
using ShopASP.Domain.Interfaces;
using ShopASP.Web.Models;

namespace ShopASP.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderRepository.GetAll();
            var orderViewModels = orders.Select(order => new OrderViewModel
            {
                OrderID = order.ID,
                UserID = order.UserId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Products = order.OrderProducts.Select(op => new OrderProductViewModel
                {
                    ProductName = op.Product.Name,
                    ProductCost = op.Product.Cost,
                    Quantity = op.Quantity
                }).ToList()
            }).ToList();

            return View(orderViewModels);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetByID(int id)
        {
            var order = await _orderRepository.GetByID(id);

            if (order == null)
            {
                return NotFound();
            }

            var orderViewModel = new OrderViewModel
            {
                OrderID = order.ID,
                UserID = order.UserId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Products = order.OrderProducts.Select(op => new OrderProductViewModel
                {
                    ProductName = op.Product.Name,
                    ProductCost = op.Product.Cost,
                    Quantity = op.Quantity
                }).ToList()
            };

            return View(orderViewModel);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            var orderViewModel = new OrderViewModel
            {
                UserID = User.Identity.Name,
                OrderDate = DateTime.Now,
                Products = new List<OrderProductViewModel>()
            };
            return View(orderViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderViewModel orderModel)
        {
            if (!ModelState.IsValid)
            {
                return View(orderModel);
            }

            var totalAmount = orderModel.Products.Sum(p => p.TotalCost);
            orderModel.TotalAmount = (int)totalAmount;

            var order = new Order
            {
                UserId = orderModel.UserID,
                OrderDate = orderModel.OrderDate,
                TotalAmount = orderModel.TotalAmount
            };

            foreach (var product in orderModel.Products)
            {
                var orderProduct = new OrderProduct
                {
                    Order = order,
                    ProductID = product.ProductID,
                    Quantity = product.Quantity
                };

                order.OrderProducts.Add(orderProduct);
            }

            await _orderRepository.Create(order);

            return RedirectToAction("OrderConfirmation");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var order = await _orderRepository.GetByID(id);

            if (order == null)
            {
                return NotFound();
            }

            var orderViewModel = new OrderViewModel
            {
                OrderID = order.ID,
                UserID = order.UserId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Products = order.OrderProducts.Select(op => new OrderProductViewModel
                {
                    ProductName = op.Product.Name,
                    ProductCost = op.Product.Cost,
                    Quantity = op.Quantity
                }).ToList()
            };

            return View(orderViewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, OrderViewModel orderModel)
        {
            if (id != orderModel.OrderID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(orderModel);
            }

            var order = await _orderRepository.GetByID(id);

            if (order == null)
            {
                return NotFound();
            }

            order.OrderDate = orderModel.OrderDate;
            order.TotalAmount = orderModel.TotalAmount;

            order.OrderProducts.Clear();
            foreach (var product in orderModel.Products)
            {
                var orderProduct = new OrderProduct
                {
                    Order = order,
                    ProductID = product.ProductID,
                    Quantity = product.Quantity
                };

                order.OrderProducts.Add(orderProduct);
            }

            await _orderRepository.Update(order);

            TempData["SuccessMessage"] = "Заказ был обновлен!";
            return RedirectToAction("GetByID", new { id = order.ID });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderRepository.GetByID(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(new OrderViewModel { OrderID = order.ID });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _orderRepository.Delete(id);
            TempData["SuccessMessage"] = "Заказ был удален!";
            return RedirectToAction("GetAll");
        }
    }
}
