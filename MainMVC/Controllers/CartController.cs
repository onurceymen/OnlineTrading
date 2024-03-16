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


        [HttpPost]
        public IActionResult AddProductToCart(CartViewModel model)
        {
            // Implement validation and error handling as needed
            _cartService.AddProductToCart(model.UserId, model.ProductId, model.Quantity);
            return RedirectToAction("Index", "Home"); // Redirect to home page or cart page
        }

        [HttpPost]
        public IActionResult EditCartItem(CartViewModel model)
        {
            // Implement validation and error handling as needed
            _cartService.EditCartItem(model.CartItemId, model.Quantity);
            return RedirectToAction("Index", "Cart"); // Redirect to cart page
        }
    }
}
