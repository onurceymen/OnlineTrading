using MainMVC.ViewModels.CartViewModel;

namespace MainMVC.Contracts
{
    public interface ICartService
    {
        Task<bool> AddProductToCartAsync(CartViewModel model);
        Task<bool> EditCartAsync(CartViewModel model);

    }
}
