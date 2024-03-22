using Data.Entity;
using MainMVC.ViewModels.ProductViewModel;

namespace MainMVC.Contracts
{
    public interface IProductService
    {
        Task<bool> CreateProductAsync(ProductViewModel model);
        Task<bool> UpdateProductAsync(ProductViewModel model);
        Task<bool> DeleteProductAsync(int productId);
        Task<bool> AddProductCommentAsync(ProductCommentViewModel model);
        Task<ProductViewModel> GetProductDetailAsync(int productId);
        Task<IEnumerable<ProductViewModel>> GetAllProductsAsync();
        Task<IEnumerable<ProductViewModel>> GetProductsByCategoryAsync(int categoryId);
        Task<IEnumerable<ProductViewModel>> GetUserProductsAsync(int userId);
        Task<bool> UpdateProductStatusAsync(int productId, bool isEnabled);
    }
}
