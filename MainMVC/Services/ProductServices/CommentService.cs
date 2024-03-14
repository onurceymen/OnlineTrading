using Data.Context;
using Data.Entity;
using MainMVC.ViewModels.ProductViewModel;
using Microsoft.EntityFrameworkCore;

namespace MainMVC.Services.ProductServices
{
    public class CommentService
    {
        private readonly AppDbContext _dbContext;

        public CommentService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CommentViewModel>> GetAllCommentsForProduct(int productId)
        {
            var comments = _dbContext.ProductComments
                .Where(c => c.Products.Id == productId)
                .Select(c => new CommentViewModel
                {
                    UserName = c.Users.FirstName, // Assuming you have a UserName property in your User entity
                    Content = c.Text,
                    ProductId = c.Products.Id
                })
                .ToListAsync();

            return await comments;
        }

        public async Task<bool> AddCommentAsync(string userName, string content, int productId, byte starCount)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            if (product == null)
            {
                return false; // Ürün bulunamadı, yorum eklenemedi
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.FirstName == userName);
            if (user == null)
            {
                return false; // Kullanıcı bulunamadı, yorum eklenemedi
            }

            var comment = new ProductComment
            {
                Text = content,
                StarCount = starCount,
                IsConfirmed = false,
                CreatedAt = DateTime.Now,
                Products = product,
                Users = user
            };

            _dbContext.ProductComments.Add(comment);
            await _dbContext.SaveChangesAsync();

            return true; // Yorum başarıyla eklendi
        }


        public async Task<bool> ConfirmCommentAsync(int commentId)
        {
            var comment = _dbContext.ProductComments.FirstOrDefault(c => c.Id == commentId);
            if (comment != null)
            {
                comment.IsConfirmed = true;
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
