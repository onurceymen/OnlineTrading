using MainMVC.Services.CartServices;
using MainMVC.ViewModels.CartViewModel;
using Microsoft.AspNetCore.Mvc;
using MainMVC.ViewModels.CartViewModel;


namespace MainMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddProductToCartAsync()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProductToCartAsync(CartViewModel model)
        {
            _cartService.AddProductToCartAsync(model);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult EditCartItem()
        {
            return RedirectToAction("Index", "Cart"); 
        }

        [HttpPost]
        public IActionResult EditCartItem(CartViewModel model)
        {
            return RedirectToAction("Index", "Cart"); 
        }
    }
}
