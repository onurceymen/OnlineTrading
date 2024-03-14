using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MainMVC.Services.ProductServices
{
    public class DeleteService
    {
        private readonly AppDbContext _dbContext;

        public DeleteService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
            {
                return false;
            }

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
