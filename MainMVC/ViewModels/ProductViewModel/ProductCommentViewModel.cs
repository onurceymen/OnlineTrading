namespace MainMVC.ViewModels.ProductViewModel
{
    public class ProductCommentViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
