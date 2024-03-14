using Data.Context;
using MainMVC.ViewModels.HomeViewModel;
using MainMVC.ViewModels.ProductViewModel;

namespace MainMVC.Services.HomeServices
{
    public class ProductDetailService
    {
        private readonly AppDbContext _dbContext;

        public ProductDetailService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ProductDetailViewModel GetProductDetail(int productId)
        {
            var productDetail = _dbContext.Products
                .Where(p => p.Id == productId)
                .Select(p => new ProductDetailViewModel
                {
                    CategoryName = p.Categorys.Name,
                    Title = p.Name,
                    Id = p.Id,
                    Description = p.Details,
                    ImageUrl = p.ProductImages.FirstOrDefault().Url,
                    Comments = p.ProductComments.Select(pc => new CommentViewModel
                    {
                        UserName = pc.Users.FirstName,
                        Content = pc.Text,
                        StarCount = pc.StarCount,
                        IsConfirmed = pc.IsConfirmed
                    }).ToList()
                })
                .FirstOrDefault();

            return productDetail;
        }
    }
}
