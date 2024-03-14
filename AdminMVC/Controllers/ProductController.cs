using Microsoft.AspNetCore.Mvc;
using AdminMVC.Services.ProductServices;
using AdminMVC.ViewModels.ProductViewModels;

namespace AdminMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductDeleteService _productDeleteService;

        public ProductController(ProductDeleteService productDeleteService)
        {
            _productDeleteService = productDeleteService;
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productDeleteService.GetProductForDeleteAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _productDeleteService.DeleteProductAsync(id);
                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while deleting the product.");
                return View(id);
            }
        }
    }
}
