using AdminMVC.ViewModels.CategoryViewModels;
using Data.Context;
using Data.Entity;

namespace AdminMVC.Services.CategoryServices
{
    public class CategoryCreateService
    {
        private readonly AppDbContext _dbContext;

        public CategoryCreateService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateCategoryAsync(CategoryCreateViewModel model)
        {
            var category = new Category
            {
                Name = model.Name,
                Color = model.Color,
                IconCssClass = model.IconCssClass,
                CreatedAt = DateTime.Now
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
