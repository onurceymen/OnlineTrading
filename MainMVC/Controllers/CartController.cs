using MainMVC.ViewModels.CartViewModel;
using Microsoft.AspNetCore.Mvc;
using Data.Constants;
using Microsoft.AspNetCore.Authorization;
using MainMVC.Contracts;


namespace MainMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [Authorize(Roles = RoleConstant.BuyerRole + "," + RoleConstant.SellerRole)]
        public async Task<IActionResult> Index(int userId)
        {
            var cartViewModel = await _cartService.GetCartDetailsAsync(userId);
            return View(cartViewModel);
        }

        [Authorize(Roles = RoleConstant.BuyerRole + RoleConstant.SellerRole)]
        [HttpGet]
        public IActionResult AddProductToCartAsync()
        {
            return View();
        }

        [Authorize(Roles = RoleConstant.BuyerRole + RoleConstant.SellerRole)]
        [HttpPost]
        public async Task<IActionResult> AddProductToCartAsync(CartViewModel model)
        {
            await _cartService.AddProductToCartAsync(model);
            return RedirectToAction("Index", new { userId = model.UserId });
        }

        [HttpGet]
        [Authorize(Roles = RoleConstant.BuyerRole + RoleConstant.SellerRole)]
        public async Task<IActionResult> RemoveProductFromCartAsync()
        {
            return View();

        }

        [Authorize(Roles = RoleConstant.BuyerRole + RoleConstant.SellerRole)]
        [HttpPost]
        public async Task<IActionResult> RemoveProductFromCartAsync(int cartItemId, int userId)
        {
            await _cartService.RemoveProductFromCartAsync(cartItemId);
            return RedirectToAction("Index", new { userId = userId });
        }

        [Authorize(Roles = RoleConstant.BuyerRole + RoleConstant.SellerRole)]
        [HttpGet]
        public IActionResult UpdateCartItemQuantityAsync()
        {
            return RedirectToAction("Index", "Cart"); 
        }

        [Authorize(Roles = RoleConstant.BuyerRole + RoleConstant.SellerRole)]
        [HttpPost]
        public async Task<IActionResult> UpdateCartItemQuantityAsync(int cartItemId, byte quantity, int userId)
        {
            await _cartService.UpdateCartItemQuantityAsync(cartItemId, quantity);
            return RedirectToAction("Index", new { userId = userId });
        }
    }
}
