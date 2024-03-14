using Data.Context;
using Data.Entity;
using MainMVC.ViewModels.ProductViewModel;

namespace MainMVC.Services.ProductServices
{
    public class CreateService
    {
        private readonly AppDbContext _dbContext;

        public CreateService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> CreateProductAsync(CreateViewModel model, User seller, Category category)
        {
            // Model validation
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            // Create a new product
            var product = new Product
            {
                Name = model.Name,
                Details = model.Description,
                Price = model.Price,
                Seller = seller,
                Categorys = category,
                StockAmount = 0, // Assuming new products start with zero stock
                CreatedAt = DateTime.UtcNow,
                Enabled = true // Assuming new products are enabled by default
            };

            // Save the product to the database
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }
    }
}
