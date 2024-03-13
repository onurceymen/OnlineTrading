using Microsoft.AspNetCore.Mvc;

namespace AdminMVC.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
