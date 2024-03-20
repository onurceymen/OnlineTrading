using Data.Entity;
using MainMVC.Services.ProductServices;
using MainMVC.ViewModels.ProductViewModel;
using Microsoft.AspNetCore.Mvc;


namespace MainMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ProductViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var product = await _productService.CreateProductAsync(model);

            if (product == null)
            {
                ModelState.AddModelError("", "Ürün oluşturulamadı.");
                return View(model);
            }

            return RedirectToAction("Details", new { id = model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int productId)
        {
            var product = await _productService.GetProductDetailAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            var model = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Details = product.Details,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var success = await _productService.EditProductAsync(model);
            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction("Home","Index");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int productId)
        {
            var result = await _productService.DeleteProductAsync(productId);

            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details()
        {
            return View();

        }

        [Route("/Product/Details/{id}")]
        [HttpPost]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductDetailAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpGet]
        public IActionResult AddComment()
        {
            return View(new ProductViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(ProductViewModel model)
        {
            return RedirectToAction("Index", "Home"); 
        }
    }
}
