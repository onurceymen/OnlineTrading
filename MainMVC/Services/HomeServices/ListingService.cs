using Data.Context;
using Data.Entity;

namespace MainMVC.Services.HomeServices
{
    public class ListingService
    {
        private readonly AppDbContext _dbContext;

        public ListingService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> GetAllProducts()
        {
            return _dbContext.Products.ToList();
        }
    }
}
