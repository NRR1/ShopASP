using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopASP.Application.Interfaces;
using ShopASP.Web.Models;

namespace ShopASP.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }
        public async Task<IActionResult> Index()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if(userRole == "Admin")
            {
                ViewBag.Message = "Вы админ._.";
            }
            else
            {
                ViewBag.Message = "Авторизуйтесь для продолжения";
            }
            var products = await productService.GetAllAsync();
            var viewModel = new ProductListViewModel
            {
                Products = products
            };
            return View(viewModel);
        }


        public async Task<IActionResult> Details(int id)
        {
            var product = await productService.GetByIDAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new DetailProductViewModel { };
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")] 
        public IActionResult Create()
        {
            var prod = new ProductViewModel
            {
                Product = new Application.DTO.ProductDTO()
            };
            return View(prod);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                await productService.CreateAsync(model.Product);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        //public IActionResult Create()
        //{
        //    var user = new ProductListViewModel();
        //    var autoUser = user.User.dRoleNames.ToString();
        //    if(autoUser != "Admin")
        //    {
        //        return Forbid();
        //    }
        //    var prod = new ProductViewModel
        //    {
        //        Product = new Application.DTO.ProductDTO()
        //    };
        //    return View(prod);
        //    //var user = new ProductListViewModel();
        //    //var autorizeUser = user.User.uRoleName;
        //    //if(autorizeUser != "Admin")
        //    //{
        //    //    return Forbid();
        //    //}
        //    //var view = new ProductViewModel
        //    //{
        //    //    Product = new ProductDTO()
        //    //};
        //    //return View(/*view*/);
        //    //Создается объект для передачи между слоями, передаётся на представление
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(ProductViewModel model)
        //{
        //    // Проверяем, имеет ли пользователь права администратора
        //    var user = new ProductListViewModel();
        //    var autorizeUser = user.User.uRoleName;

        //    if (autorizeUser != "Admin")
        //    {
        //        // Если нет, запрещаем доступ
        //        return Forbid();
        //    }

        //    // Проверяем, прошла ли модель валидацию
        //    if (ModelState.IsValid)
        //    {
        //        // Если да, вызываем метод сервиса для создания продукта
        //        await productService.CreateAsync(model.Product);

        //        // Перенаправляем на метод Index после успешного создания
        //        return RedirectToAction(nameof(Index));
        //    }

        //    // Если модель не валидна, возвращаем представление с моделью
        //    return View(model);
        //}




        public async Task<IActionResult> Edit(int id)
        {
            //var user = new ProductListViewModel();
            //var autorizeUser = user.User.uRoleName;
            //if (autorizeUser != "Admin")
            //{
            //    return Forbid();
            //}
            //var prod = await productService.GetByIDAsync(id);
            //if (prod == null)
            //{
            //    return NotFound();
            //}

            var viewModel = new ProductViewModel
            {
                //Product = prod
            };
            return View(viewModel);
            //Передаётся объект модели на представление путем подставки через метод GetByID
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            var user = new ProductListViewModel();
            //var autorizeUser = user.User.uRoleName;
            //if (autorizeUser != "Admin")
            //{
            //    return Forbid();
            //}

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await productService.UpdateProduct(model.Product);

            return RedirectToAction(nameof(Index));
            //Передаётся в метод через ViewModel
        }


        public async Task<IActionResult> Delete(int id)
        {
            var user = new ProductListViewModel();
            //var autorizeUser = user.User.uRoleName.ToString();
            //if (autorizeUser != "Admin")
            //{
            //    return Forbid();
            //}


            if (id == null)
            {
                return NotFound();
            }
            var dprod = await productService.GetByIDAsync(id);
            if (dprod == null)
            {
                return NotFound();
            }

            var model = new DetailProductViewModel { };
            //{
            //    Id = dprod.pID,
            //    Name = dprod.pName,
            //    Description = dprod.pDescription,
            //    Price = dprod.pCost,
            //    Quantity = dprod.pQuantity,
            //};
            return View(model);
            //Аналогично методу Update
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = new ProductListViewModel();
            //var autorizeUser = user.User.uRoleName;
            //if (autorizeUser != "Admin")
            //{
            //    return Forbid();
            //}


            var prod = await productService.GetByIDAsync(id);
            if(prod != null)
            {
                await productService.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
            //Передается методу delete значение ID для работы и последующего удаления из БД
        }
    }
}
