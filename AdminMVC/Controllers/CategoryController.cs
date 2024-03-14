using AdminMVC.Services.CategoryServices;
using AdminMVC.ViewModels.CategoryViewModels;
using Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace AdminMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryCreateService _categoryCreateService;
        private readonly CategoryEditService _categoryEditService;
        private readonly CategoryDeleteService _categoryDeleteService;
        private readonly AppDbContext _dbContext;


        public CategoryController(CategoryCreateService categoryCreateService, CategoryEditService categoryEditService, CategoryDeleteService categoryDeleteService, AppDbContext appDbContext)
        {
            _categoryCreateService = categoryCreateService;
            _categoryEditService = categoryEditService;
            _categoryDeleteService = categoryDeleteService;
            _dbContext = appDbContext;


        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CategoryCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _categoryCreateService.CreateCategoryAsync(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while creating the category.");
                return View(model);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await _categoryEditService.GetCategoryForEditAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost]
        [Route("/category/{id}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _categoryEditService.UpdateCategoryAsync(model);
                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating the category.");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var viewModel = await _categoryDeleteService.GetCategoryForDeleteAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _categoryDeleteService.DeleteCategoryAsync(id);
                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while deleting the category.");
                return RedirectToAction("Delete", new { id });
            }
        }
    }
}
