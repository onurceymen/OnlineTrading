using System.ComponentModel.DataAnnotations;

namespace AdminMVC.ViewModels.CategoryViewModels
{
    public class CategoryEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 3)]
        public string Color { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string IconCssClass { get; set; }
    }
}
