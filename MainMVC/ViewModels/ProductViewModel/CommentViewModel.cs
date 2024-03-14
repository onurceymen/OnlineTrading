namespace MainMVC.ViewModels.ProductViewModel
{
    public class CommentViewModel
    {
        public string UserName { get; set; }
        public string Content { get; set; }
        public int ProductId { get; set; }
        public byte StarCount { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
