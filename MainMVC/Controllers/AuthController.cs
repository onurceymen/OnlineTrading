using Microsoft.AspNetCore.Mvc;
using MainMVC.Services.AuthServices;
using MainMVC.ViewModels.AuthViewModels;
using Microsoft.AspNetCore.Identity;
using Data.Entity;
using System.Threading.Tasks;

namespace MainMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AuthViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.RegisterAsync(model);
                if (result.Succeeded)
                {
                    // Registration successful, redirect to login page
                    return RedirectToAction(nameof(Login));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            // If registration fails, return to registration page with errors
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AuthViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.LoginAsync(model);
                if (result.Succeeded)
                {
                    // Login successful, redirect to home page
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid login attempt");
            }

            // If login fails, return to login page with errors
            return View(model);
        }

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
            if (ModelState.IsValid)
            {
                var user = await _authService.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }

                var token = await _authService.GeneratePasswordResetTokenAsync(user);
                // Send token to user's email for password reset

                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            // If model state is invalid, return to forgot password page with errors
            return View(model);
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
    }
}
