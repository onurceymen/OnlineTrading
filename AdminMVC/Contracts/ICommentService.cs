using AdminMVC.ViewModels.CommentViewModels;

namespace AdminMVC.Contracts
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentViewModel>> GetAllCommentsAsync();
        Task<CommentViewModel> GetCommentByIdAsync(int id);
        Task<CommentViewModel> CreateCommentAsync(CommentViewModel comment);
        Task UpdateCommentAsync(CommentViewModel comment);
        Task DeleteCommentAsync(int id);
    }
}
