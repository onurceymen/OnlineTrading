using AdminMVC.Contracts;
using AdminMVC.ViewModels.ProductViewModels;

namespace AdminMVC.Services.ProductServices
{
    public class ProductService : IProductService
    {
        public Task<ProductViewModel> CreateProductAsync(ProductViewModel product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductViewModel>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductViewModel> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductAsync(ProductViewModel product)
        {
            throw new NotImplementedException();
        }
    }
}
