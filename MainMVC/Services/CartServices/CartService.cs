using Data.Context;
using Data.Entity;
using Data.Services;
using MainMVC.Contracts;
using MainMVC.ViewModels.CartViewModel;
using MainMVC.ViewModels.ProductViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;

namespace MainMVC.Services.CartServices
{
    public class CartService : ICartService
    {
        private readonly DataRepository<CartItem> _cartItemRepository;

        public CartService(AppDbContext dbContext)
        {
            _cartItemRepository = new DataRepository<CartItem>(dbContext);
        }

        public async Task<bool> AddProductToCartAsync(CartViewModel model)
        {
            var cartItem = (await _cartItemRepository.FindAsync(c => c.UserId == model.UserId && c.ProductId == model.ProductId)).FirstOrDefault();

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    UserId = model.UserId,
                    ProductId = model.ProductId,
                    Quantity = model.Quantity,
                    CreatedAt = DateTime.Now
                };
                await _cartItemRepository.CreateAsync(cartItem);
            }
            else
            {
                cartItem.Quantity += model.Quantity;
                await _cartItemRepository.UpdateAsync(cartItem);
            }

            return true;
        }

        public async Task<bool> RemoveProductFromCartAsync(int cartItemId)
        {
            await _cartItemRepository.DeleteAsync(cartItemId);
            return true;
        }

        public async Task<bool> UpdateCartItemQuantityAsync(int cartItemId, byte quantity)
        {
            var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                await _cartItemRepository.UpdateAsync(cartItem);
                return true;
            }
            return false;
        }

        public async Task<CartViewModel> GetCartDetailsAsync(int userId)
        {
            var cartItems = (await _cartItemRepository.FindAsync(c => c.UserId == userId)).Select(c => new CartItemViewModel
            {
                CartItemId = c.Id,
                ProductId = c.ProductId,
                ProductName = c.Product.Name,
                Price = c.Product.Price,
                Quantity = c.Quantity,
                TotalPrice = c.Quantity * c.Product.Price
            }).ToList();

            return new CartViewModel
            {
                UserId = userId,
                CartItemId = cartItems,
                TotalPrice = cartItems.Sum(c => c.TotalPrice)
            };
        }
    }
}
