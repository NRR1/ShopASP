using Microsoft.AspNetCore.Mvc;
using ShopASP.Application.Interface;
using ShopASP.Web.Models;

namespace ShopASP.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService service;
        public ProductController(IProductService _service)
        {
            service = _service;
        }
        public async Task<IActionResult> Index()
        {
            var products = await service.GetAll();
            var vm = products.Select(dto => ProductListViewModel.FromDTO(dto)).ToList();
            return View(vm);
        }


    }
}
