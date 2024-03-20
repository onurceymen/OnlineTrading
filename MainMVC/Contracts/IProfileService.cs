using MainMVC.ViewModels.OrderViewModel;
using MainMVC.ViewModels.ProductViewModel;
using MainMVC.ViewModels.ProfileViewModels;

namespace MainMVC.Contracts
{
    public interface IProfileService
    {
        Task<ProfileViewModel> GetUserProfileAsync(int userId);
        Task<bool> UpdateUserProfileAsync(ProfileViewModel model);
        Task<IEnumerable<OrderViewModel>> GetUserOrdersAsync(int userId);
        Task<IEnumerable<ProductViewModel>> GetUserProductsAsync(int userId);
    }
}
