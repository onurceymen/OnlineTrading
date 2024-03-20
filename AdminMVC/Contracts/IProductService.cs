using AdminMVC.ViewModels.ProductViewModels;

namespace AdminMVC.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllProductsAsync();
        Task<ProductViewModel> GetProductByIdAsync(int id);
        Task<ProductViewModel> CreateProductAsync(ProductViewModel product);
        Task UpdateProductAsync(ProductViewModel product);
        Task DeleteProductAsync(int id);
    }
}
