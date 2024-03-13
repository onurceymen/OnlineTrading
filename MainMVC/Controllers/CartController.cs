using Microsoft.AspNetCore.Mvc;

namespace MainMVC.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}
