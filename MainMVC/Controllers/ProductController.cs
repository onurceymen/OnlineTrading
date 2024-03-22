using Data.Constants;
using MainMVC.Contracts;
using MainMVC.ViewModels.ProductViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        [Authorize(Roles = RoleConstant.SellerRole)]
        [HttpGet]
        public IActionResult Create()
        {
            return View(new ProductViewModel());
        }

        [Authorize(Roles = RoleConstant.SellerRole)]
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            await _productService.CreateProductAsync(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProductDetailAsync(id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            await _productService.UpdateProductAsync(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = RoleConstant.SellerRole)]
        [HttpGet]
        public IActionResult Details()
        {
            return View();

        }

        [Authorize(Roles = RoleConstant.SellerRole)]
        [Route("/Product/Details/{id}")]
        [HttpPost]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductDetailAsync(id);
            return View(product);
        }


        [HttpGet]
        [Authorize(Roles = RoleConstant.SellerRole)]
        public async Task<IActionResult> UserProducts(int userId)
        {
            var products = await _productService.GetUserProductsAsync(userId);
            return View(products);
        }

        [Authorize(Roles = RoleConstant.SellerRole)]
        [HttpPost]
        public async Task<IActionResult> UpdateProductStatus(int productId, bool isEnabled)
        {
            await _productService.UpdateProductStatusAsync(productId, isEnabled);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = RoleConstant.SellerRole + RoleConstant.BuyerRole)]
        [HttpGet]
        public IActionResult AddComment()
        {
            return View(new ProductViewModel());
        }

        [Authorize(Roles = RoleConstant.SellerRole + RoleConstant.BuyerRole)]
        [HttpPost]
        public async Task<IActionResult> AddComment(ProductViewModel model)
        {
            return RedirectToAction("Index", "Home"); 
        }
    }
}
