using Data.Context;
using Data.Entity;
using MainMVC.Contracts;
using MainMVC.ViewModels.ProductViewModel;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace MainMVC.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _dbContext;

        public ProductService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateProductAsync(ProductViewModel model)
        {
            var existingProduct = await _dbContext.Products.FindAsync(model.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = model.Name;
                existingProduct.Price = model.Price;
                existingProduct.Details = model.Details;

                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;



            /* try
            {
                var product = new Product
                {
                    Name = model.Name,
                    Details = model.Description,
                    Price = model.Price,
                    Seller = seller,
                    Category = category,
                    StockAmount = 0, // Yeni ürünler varsayılan olarak stokta yok
                    CreatedAt = DateTime.UtcNow,
                    Enabled = true // Yeni ürünler varsayılan olarak aktif
                };

                _dbContext.Products.Add(product);
                await _dbContext.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {
                // Hata durumunda loglama veya gerekli işlemler yapılabilir
                return null;
            }*/
        }
        public async Task<bool> EditProductAsync(ProductViewModel model)
        {
            try
            {
                var product = await _dbContext.Products.FindAsync(model.Id);
                if (product == null)
                {
                    return false; // Ürün bulunamadı
                }

                product.Name = model.Name;
                product.Details = model.Details;
                product.Price = model.Price;

                await _dbContext.SaveChangesAsync();
                return true; // Başarıyla güncellendi
            }
            catch (Exception ex)
            {
                // Hata durumunda loglama veya gerekli işlemler yapılabilir
                return false;
            }

            /*var existingProduct = await _dbContext.Products.FindAsync(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Details = product.Details;
                existingProduct.StockAmount = product.StockAmount;

                await _dbContext.SaveChangesAsync();
            }*/
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            try
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
                if (product == null)
                {
                    return false; // Ürün bulunamadı
                }

                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();

                return true; // Başarıyla silindi
            }
            catch (Exception ex)
            {
                // Hata durumunda loglama veya gerekli işlemler yapılabilir
                return false;
            }
        }

        public async Task<bool> AddProductCommentAsync(ProductCommentViewModel model)
        {
            var product = await _dbContext.Products.FindAsync(model.ProductId);
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.FirstName == model.UserName);

            if (product == null || user == null)
            {
                return false; 
            }

            var comment = new ProductComment
            {
                IsConfirmed = false,
                CreatedAt = DateTime.Now,
                Product = product,
                User = user
            };

            _dbContext.ProductComments.Add(comment);
            await _dbContext.SaveChangesAsync();

            return true; 
        }

        public Task<ProductViewModel> GetProductDetailAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductViewModel>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductViewModel>> GetProductsByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductViewModel>> GetAllCommentsForProduct(int productId)
        {
            var comments = _dbContext.ProductComments
                .Where(c => c.Product.Id == productId)
                .Select(c => new ProductViewModel
                {
                    UserName = c.User.FirstName,
                    Content = c.Text,
                    ProductId = c.Product.Id,
                })
                .ToListAsync();

            return await comments;
        }
    }
}
