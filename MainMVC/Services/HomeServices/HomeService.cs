using Data.Context;
using Data.Entity;
using MainMVC.Contracts;
using MainMVC.Services.ProductServices;
using MainMVC.ViewModels.HomeViewModel;
using MainMVC.ViewModels.ProductViewModel;

namespace MainMVC.Services.HomeServices
{
    public class HomeService : IHomeServices
    {
        private readonly AppDbContext _dbContext;

        public HomeService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<ContactViewModel> GetContactInformation()
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductViewModel>> GetAllProductsListings()
        {
            throw new NotImplementedException();
        }
        public Task<ProductViewModel> GetProductDetail(int productId)
        {
            throw new NotImplementedException();
        }


    }
}
