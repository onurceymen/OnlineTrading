using Data.Context;
using Data.Entity;
using MainMVC.Contracts;
using MainMVC.ViewModels.CartViewModel;
using MainMVC.ViewModels.ProductViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;

namespace MainMVC.Services.CartServices
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _dbContext;

        public CartService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddProductToCartAsync(CartViewModel model)
        {
            var cartItem = _dbContext.CartItems.FirstOrDefault(c => c.UserId == model.UserId && c.ProductId == model.ProductId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    UserId = model.UserId,
                    ProductId = model.ProductId,
                    Quantity = model.Quantity,
                    CreatedAt = DateTime.Now
                };
                _dbContext.CartItems.Add(cartItem);
            }
            _dbContext.SaveChanges();

            return true;
        }

        public Task<bool> EditCartAsync(CartViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
