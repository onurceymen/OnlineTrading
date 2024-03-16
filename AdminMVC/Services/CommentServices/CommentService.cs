using AdminMVC.ViewModels.CommentViewModels;
using Data.Context;

namespace AdminMVC.Services.CommentServices
{
    public class CommentService
    {
        private readonly AppDbContext _dbContext;

        public CommentService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<CommentViewModel> GetComments()
        {
            return _dbContext.ProductComments
                .Select(pc => new CommentViewModel
                {
                    CommentId = pc.Id,
                    ProductId = pc.ProductId,
                    ProductName = pc.Product.Name,
                    UserId = pc.UserId,
                    UserName = pc.User.FirstName + " " + pc.User.LastName,
                    Text = pc.Text,
                    StarCount = pc.StarCount,
                    IsConfirmed = pc.IsConfirmed,
                    CreatedAt = pc.CreatedAt
                })
                .ToList();
        }

        public void ApproveComment(int commentId)
        {
            var comment = _dbContext.ProductComments.FirstOrDefault(pc => pc.Id == commentId);
            if (comment != null)
            {
                comment.IsConfirmed = true;
                _dbContext.SaveChanges();
            }
        }
    }
}
