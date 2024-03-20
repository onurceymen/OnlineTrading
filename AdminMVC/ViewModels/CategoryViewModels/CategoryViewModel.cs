using System.ComponentModel.DataAnnotations;

namespace AdminMVC.ViewModels.CategoryViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

   
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

      
        [StringLength(6, MinimumLength = 3)]
        public string Color { get; set; }

      
        [StringLength(50, MinimumLength = 2)]
        public string IconCssClass { get; set; }


       
    }
}
