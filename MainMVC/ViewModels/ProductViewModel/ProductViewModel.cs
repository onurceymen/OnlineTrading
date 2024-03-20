using Data.Entity;

namespace MainMVC.ViewModels.ProductViewModel
{
    public class ProductViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public List<Product> Products { get; set; }


        public string UserName { get; set; }
        public string Content { get; set; }
        public int ProductId { get; set; }
        public byte StarCount { get; set; }
        public bool IsConfirmed { get; set; }


    }
}
