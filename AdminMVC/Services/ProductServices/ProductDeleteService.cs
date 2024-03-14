using AdminMVC.ViewModels.ProductViewModels;
using Data.Context;

namespace AdminMVC.Services.ProductServices
{
    public class ProductDeleteService
    {
        private readonly AppDbContext _dbContext;

        public ProductDeleteService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductDeleteViewModel> GetProductForDeleteAsync(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }

            return new ProductDeleteViewModel
            {
                Id = product.Id,
                Name = product.Name
            };
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                throw new ArgumentException("Product not found");
            }

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}

