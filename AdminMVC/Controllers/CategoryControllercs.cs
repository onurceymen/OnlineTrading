using Microsoft.AspNetCore.Mvc;

namespace AdminMVC.Controllers
{
    public class CategoryControllercs : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [Route("/category/{id}/edit")]
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
