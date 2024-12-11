using Microsoft.AspNetCore.Mvc;

namespace ShopASP.Web.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
