using Microsoft.AspNetCore.Mvc;
using ShopASP.Application.DTO;
using ShopASP.Application.Interface;
using ShopASP.Web.Models;

namespace ShopASP.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService service;
        public OrderController(IOrderService _service)
        {
            service = _service;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<OrderDTO> orders = await service.GetAll();
            var vm = orders.Select(dto => OrderListViewModel.FromDTO(dto)).ToList();
            return View(vm);
        }

        public async Task<IActionResult> Details(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var order = await service.GetByID(id);
            if(order == null)
            {
                return NotFound();
            }
            var vm = OrderViewModel.FromDTO(order);
            return View(vm);
        }

        [HttpGet]
        public IActionResult Create() => View(new ProductViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,UserID,OrderDate,TotalAmount")] OrderViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            OrderDTO? order = model.ToDTO();
            await service.Create(order);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var order = await service.GetByID(id);
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
                await service.Update(dto);
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
            var order = await service.GetByID(id);
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

            var order = await service.GetByID(id);
            if(order == null)
            {
                return NotFound();
            }
            await service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
