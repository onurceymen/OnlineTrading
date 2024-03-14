using Data.Entity;

namespace MainMVC.ViewModels.ProductViewModel
{
    public class DeleteViewModel
    {
        public int Id { get; set; }
        public List<Product> products { get; set; }

    }
}
