using AdminMVC.Contracts;
using AdminMVC.ViewModels.CommentViewModels;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AdminMVC.Services.CommentServices
{
    public class CommentService :ICommentService
    {
        private readonly AppDbContext _dbContext;

        public CommentService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CommentViewModel>> GetAllCommentsAsync()
        {
            return await _dbContext.ProductComments
                .Select(pc => new CommentViewModel()
                {
                    CommentId = pc.Id,
                    ProductId= pc.Product.Id,
                    ProductName = pc.Product.Name,
                    UserId = pc.User.Id,
                    UserName = pc.User.FirstName + " " + pc.User.LastName,
                    Text = pc.Text,
                    StarCount = pc.StarCount,
                    IsConfirmed = pc.IsConfirmed,
                    CreatedAt = pc.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<CommentViewModel> GetCommentByIdAsync(int id)
        {
            if (id == 0)
            {
                return null;
            }

            var comment = await _dbContext.ProductComments
                .Where(pc => pc.Id == id)
                .Select(pc => new CommentViewModel()
                {
                    CommentId = pc.Id,
                    ProductId = pc.Product.Id,
                })
                .FirstOrDefaultAsync();

            return comment;

        }

        public Task<CommentViewModel> CreateCommentAsync(CommentViewModel comment)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCommentAsync(CommentViewModel comment)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCommentAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
