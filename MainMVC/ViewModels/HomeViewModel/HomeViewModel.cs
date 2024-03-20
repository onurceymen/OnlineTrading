using Data.Entity;
using MainMVC.ViewModels.ProductViewModel;

namespace MainMVC.ViewModels.HomeViewModel
{
    public class HomeViewModel
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<Product> Products { get; set; }
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }





    }
}
