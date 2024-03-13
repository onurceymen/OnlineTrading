using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class ProductComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("ProductId")]
        public Product Products { get; set; }

        [ForeignKey("UserId")]
        public User Users { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 2)]
        public string Text { get; set; }

        [Range(1, 5, ErrorMessage = "Star count must be between 1 and 5.")]
        [Required]
        public byte StarCount { get; set; }

        [Required]
        public bool IsConfirmed { get; set; } = false;

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
