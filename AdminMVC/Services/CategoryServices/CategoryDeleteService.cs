using AdminMVC.ViewModels.CategoryViewModels;
using Data.Context;

namespace AdminMVC.Services.CategoryServices
{
    public class CategoryDeleteService
    {
        private readonly AppDbContext _dbContext;

        public CategoryDeleteService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CategoryDeleteViewModel> GetCategoryForDeleteAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null)
            {
                throw new ArgumentException("Category not found");
            }

            var viewModel = new CategoryDeleteViewModel
            {
                Id = category.Id,
                Name = category.Name
            };

            return viewModel;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null)
            {
                throw new ArgumentException("Category not found");
            }

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();

            return true;
        }

    }
}
