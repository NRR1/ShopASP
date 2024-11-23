using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Mvc;
using ShopASP.Application.DTO;
using ShopASP.Application.Interfaces;
using ShopASP.Web.Models;

/*
 Контроллер связывает между собой модели и представление
 Представление хранятся в некой папке, например Product
 Ровно такое же слово в названии имеет контроллер, что их связывает
 В представлении указываются методы, которые находятся в представлении
 
 Теперь о моделях. Они указываются в using представления и передают некоторые данные в через себя
 Например в данной структуре данные в модель передаются через DTO, которая служит для упрощения и оптимизации передачи между
 разными слоями приложения
 В контроллере в свою очередь используется интерфейс IProductService, который включает в себя набор методов
 Он определяется через приватное поле для чтения и так же в конструкторе контроллера. После через эту переменную мы можем
 возвращать методы, попутно передавая значения(по необходимости)
 */

namespace ShopASP.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllAsync();
            
            var viewModel = new ProductListViewModel
            {
                Products = products
            };
            return View(viewModel);

            /*
             * Сначала определяется переменная products от метода. После создаётся объект ViewModel,
             * который передаётся в представление
             */
        }


        public async Task<IActionResult> Details(int id)
        {
            var product = await productService.GetByIDAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new DetailProductViewModel
            {
                Name = product.pName,
                Description = product.pDescription,
                Price = product.pCost,
                Quantity = product.pQuantity
            };

            return View(viewModel);
            //Ровно тоже самое что и в Index()
        }
        
        public IActionResult Create()
        {
            var user = new ProductListViewModel();
            var autorizeUser = user.User.uRoleName;
            if(autorizeUser != "Admin")
            {
                return Forbid();
            }
            var view = new ProductViewModel
            {
                Product = new ProductDTO()
            };
            return View(view);
            //Создается объект для передачи между слоями, передаётся на представление
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            var user = new ProductListViewModel();
            var autorizeUser = user.User.uRoleName;
            if (autorizeUser != "Admin")
            {
                return Forbid();
            }
            if (ModelState.IsValid)
            {
                await productService.CreateAsync(model.Product);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
            //Та же модель передаётся в сервис
        }


        public async Task<IActionResult> Edit(int id)
        {
            var user = new ProductListViewModel();
            var autorizeUser = user.User.uRoleName;
            if (autorizeUser != "Admin")
            {
                return Forbid();
            }
            var prod = await productService.GetByIDAsync(id);
            if (prod == null)
            {
                return NotFound();
            }

            var viewModel = new ProductViewModel
            {
                Product = prod
            };
            return View(viewModel);
            //Передаётся объект модели на представление путем подставки через метод GetByID
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            var user = new ProductListViewModel();
            var autorizeUser = user.User.uRoleName;
            if (autorizeUser != "Admin")
            {
                return Forbid();
            }

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
            var autorizeUser = user.User.uRoleName.ToString();
            if (autorizeUser != "Admin")
            {
                return Forbid();
            }


            if (id == null)
            {
                return NotFound();
            }
            var dprod = await productService.GetByIDAsync(id);
            if (dprod == null)
            {
                return NotFound();
            }

            var model = new DetailProductViewModel
            {
                Id = dprod.pID,
                Name = dprod.pName,
                Description = dprod.pDescription,
                Price = dprod.pCost,
                Quantity = dprod.pQuantity,
            };
            return View(model);
            //Аналогично методу Update
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = new ProductListViewModel();
            var autorizeUser = user.User.uRoleName;
            if (autorizeUser != "Admin")
            {
                return Forbid();
            }


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
