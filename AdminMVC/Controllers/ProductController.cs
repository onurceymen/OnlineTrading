using Microsoft.AspNetCore.Mvc;
using AdminMVC.Services.ProductServices;
using AdminMVC.ViewModels.ProductViewModels;

namespace AdminMVC.Controllers
{
    public class ProductController : Controller
    {

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            return View();

        }
        
    }
}
