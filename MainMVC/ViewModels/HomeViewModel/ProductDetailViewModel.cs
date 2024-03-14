using MainMVC.ViewModels.ProductViewModel;

namespace MainMVC.ViewModels.HomeViewModel
{

    public class ProductDetailViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<CommentViewModel> Comments { get;  set; }

    }
}
