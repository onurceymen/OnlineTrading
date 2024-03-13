using Microsoft.AspNetCore.Mvc;

namespace MainMVC.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details()
        {
            // Details action implementation
            return View();
        }

        public IActionResult Edit()
        {
            // Edit action implementation
            return View();
        }

        public IActionResult MyOrders()
        {
            // MyOrders action implementation
            return View();
        }

        public IActionResult MyProducts()
        {
            // MyProducts action implementation
            return View();
        }
    }
}
