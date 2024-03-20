using AdminMVC.ViewModels.CategoryViewModels;

namespace AdminMVC.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync();
        Task<CategoryViewModel> GetCategoryByIdAsync(int id);
        Task<CategoryViewModel> CreateCategoryAsync(CategoryViewModel category);
        Task UpdateCategoryAsync(CategoryViewModel category);
        Task DeleteCategoryAsync(int id);
        /*
        Task<bool> CreateCategoryAsync(CategoryViewModel model);
        Task<bool> UpdateCategoryAsync(CategoryViewModel model);
        Task<bool> DeleteCategoryAsync(int categoryId);
        Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync();
        */
    }
}
