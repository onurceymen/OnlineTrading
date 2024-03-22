using MainMVC.ViewModels.CartViewModel;

namespace MainMVC.Contracts
{
    public interface ICartService
    {
        Task<bool> AddProductToCartAsync(CartViewModel model);
        Task<bool> RemoveProductFromCartAsync(int cartItemId);
        Task<bool> UpdateCartItemQuantityAsync(int cartItemId, byte quantity);
        Task<CartViewModel> GetCartDetailsAsync(int userId);

    }
}
