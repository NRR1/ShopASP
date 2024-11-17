using Microsoft.AspNetCore.Mvc;

namespace ShopASP.Web.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
