using Data.Context;
using Data.Entity;
using Data.Services;
using MainMVC.Contracts;
using MainMVC.ViewModels.ProductViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMVC.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly DataRepository<Product> _productRepository;
        private readonly DataRepository<ProductComment> _productCommentRepository;

        public ProductService(AppDbContext dbContext)
        {
            _productRepository = new DataRepository<Product>(dbContext);
            _productCommentRepository = new DataRepository<ProductComment>(dbContext);
        }

        public async Task<bool> CreateProductAsync(ProductViewModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                Details = model.Details,
                Price = model.Price
            };

            await _productRepository.CreateAsync(product);
            return true;
        }

        public async Task<bool> UpdateProductAsync(ProductViewModel model)
        {
            var product = await _productRepository.GetByIdAsync(model.Id);
            if (product != null)
            {
                product.Name = model.Name;
                product.Details = model.Details;
                product.Price = model.Price;
                await _productRepository.UpdateAsync(product);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            await _productRepository.DeleteAsync(productId);
            return true;
        }

        public async Task<bool> AddProductCommentAsync(ProductCommentViewModel model)
        {
            var productComment = new ProductComment
            {
                ProductId = model.ProductId,
                UserId = model.UserId,
                Content = model.Content,
                StarCount = model.StarCount,
                CreatedAt = DateTime.UtcNow,
                IsConfirmed = false
            };

            await _productCommentRepository.CreateAsync(productComment);
            return true;
        }

        public async Task<ProductViewModel> GetProductDetailAsync(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product != null)
            {
                return new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Details = product.Details,
                    Price = product.Price
                };
            }
            return null;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(product => new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Details = product.Details,
                Price = product.Price
            }).ToList();
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await _productRepository.FindAsync(product => product.CategoryId == categoryId);
            return products.Select(product => new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Details = product.Details,
                Price = product.Price
            }).ToList();
        }

        public async Task<IEnumerable<ProductViewModel>> GetUserProductsAsync(int userId)
        {
            var products = await _productRepository.FindAsync(product => product.UserId == userId);
            return products.Select(product => new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Details = product.Details,
                Price = product.Price
            }).ToList();
        }

        public async Task<bool> UpdateProductStatusAsync(int productId, bool isEnabled)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product != null)
            {
                product.IsEnabled = isEnabled;
                await _productRepository.UpdateAsync(product);
                return true;
            }
            return false;
        }
    }
}
