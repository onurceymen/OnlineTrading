using MainMVC.ViewModels.ProductViewModel;

namespace MainMVC.Contracts
{
    public interface ICommentService
    {
        Task<IEnumerable<ProductCommentViewModel>> GetProductCommentsAsync(int productId);
        Task<bool> ApproveCommentAsync(int commentId);
    }
}
