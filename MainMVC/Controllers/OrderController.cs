using Microsoft.AspNetCore.Mvc;

namespace MainMVC.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [Route("/order/{id}/details")]
        public IActionResult Details()
        {
            return View();
        }
    }
}
