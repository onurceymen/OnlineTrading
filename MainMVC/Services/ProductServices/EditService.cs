using Data.Context;
using MainMVC.ViewModels.ProductViewModel;

namespace MainMVC.Services.ProductServices
{
    public class EditService
    {
        private readonly AppDbContext _dbContext;

        public EditService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> EditProductAsync(EditViewModel model)
        {
            var product = await _dbContext.Products.FindAsync(model.Id);
            if (product == null)
            {
                return false; // Ürün bulunamadı
            }

            // Ürün bilgilerini güncelle
            product.Name = model.Name;
            product.Details = model.Details;
            product.Price = model.Price;

            await _dbContext.SaveChangesAsync();
            return true; // Başarıyla güncellendi
        }
    }
}
