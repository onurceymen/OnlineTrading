using Microsoft.AspNetCore.Mvc;

namespace MainMVC.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            // Create action implementation
            return View();
        }

        public IActionResult Edit()
        {
            // Edit action implementation
            return View();
        }

        public IActionResult Delete()
        {
            // Delete action implementation
            return View();
        }

        public IActionResult Details()
        {
            // Details action implementation
            return View();
        }
    }
}
