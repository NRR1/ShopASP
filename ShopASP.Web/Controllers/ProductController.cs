using Microsoft.AspNetCore.Mvc;
using ShopASP.Domain.Interfaces;
using ShopASP.Web.Models;

namespace ShopASP.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        public ProductController(IProductRepository _productRepository)
        {
            productRepository = _productRepository;
        }
        public async IActionResult Index()
        {
            var products = await productRepository.GetAllAsync();
            var vm = products.Select(p => new ProductListViewModel
            {
                ID = p.ID,
                
            });
        }
    }
}
