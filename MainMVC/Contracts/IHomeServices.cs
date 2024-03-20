using MainMVC.ViewModels.HomeViewModel;
using MainMVC.ViewModels.ProductViewModel;

namespace MainMVC.Contracts
{
    public interface IHomeServices
    {
        Task<ContactViewModel> GetContactInformation();
        Task<List<ProductViewModel>> GetAllProductsListings();
        Task<ProductViewModel> GetProductDetail(int productId);

    }
}
