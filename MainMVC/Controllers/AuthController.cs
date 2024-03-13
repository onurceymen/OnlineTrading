using Microsoft.AspNetCore.Mvc;

namespace MainMVC.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            // Register action implementation
            return View();
        }

        public IActionResult Login()
        {
            // Login action implementation
            return View();
        }

        public IActionResult ForgotPassword()
        {
            // ForgotPassword action implementation
            return View();
        }

        public IActionResult Logout()
        {
            // Logout action implementation
            return View();
        }
    }
}
