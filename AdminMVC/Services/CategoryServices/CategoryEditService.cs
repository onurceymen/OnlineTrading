using AdminMVC.ViewModels.CategoryViewModels;
using Data.Context;

namespace AdminMVC.Services.CategoryServices
{
    public class CategoryEditService
    {
        private readonly AppDbContext _dbContext;

        public CategoryEditService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CategoryEditViewModel> GetCategoryForEditAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null)
            {
                throw new ArgumentException("Category not found");
            }

            var viewModel = new CategoryEditViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Color = category.Color,
                IconCssClass = category.IconCssClass
            };

            return viewModel;
        }

        public async Task<bool> UpdateCategoryAsync(CategoryEditViewModel model)
        {
            var category = await _dbContext.Categories.FindAsync(model.Id);
            if (category == null)
            {
                throw new ArgumentException("Category not found");
            }

            category.Name = model.Name;
            category.Color = model.Color;
            category.IconCssClass = model.IconCssClass;

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
