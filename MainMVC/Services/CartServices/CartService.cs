using Data.Context;
using Data.Entity;
using MainMVC.ViewModels.ProductViewModel;
using Microsoft.AspNetCore.Http;

namespace MainMVC.Services.CartServices
{
    public class CartService
    {
        private readonly AppDbContext _dbContext;

        public CartService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddProductToCart(int userId, int productId, byte quantity)
        {
            var cartItem = _dbContext.CartItems.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity,
                    CreatedAt = DateTime.Now
                };
                _dbContext.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            _dbContext.SaveChanges();
        }

        public void EditCartItem(int cartItemId, byte quantity)
        {
            var cartItem = _dbContext.CartItems.FirstOrDefault(c => c.Id == cartItemId);

            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                _dbContext.SaveChanges();
            }
        }
    }
}
