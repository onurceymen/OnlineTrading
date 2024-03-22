using Microsoft.AspNetCore.Mvc;
using MainMVC.Services.AuthServices;
using MainMVC.ViewModels.AuthViewModels;
using Data.Constants;
using Microsoft.AspNetCore.Authorization;

namespace MainMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [Authorize(Roles = RoleConstant.BuyerRole)]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [Authorize(Roles = RoleConstant.BuyerRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AuthViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.RegisterAsync(model);
                if (!result)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
            }

            return View(model);
        }

        [Authorize(Roles = RoleConstant.BuyerRole + "," + RoleConstant.SellerRole + RoleConstant.AdminRole)]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [Authorize(Roles = RoleConstant.BuyerRole + "," + RoleConstant.SellerRole + RoleConstant.AdminRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AuthViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.LoginAsync(model);
                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Login failed. Please check your credentials and try again.");
            }

            return View(model);
        }

        [Authorize(Roles = RoleConstant.BuyerRole + "," + RoleConstant.SellerRole + RoleConstant.AdminRole)]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(AuthViewModel model)
        {
            return View(model);
        }

        
    }
}
