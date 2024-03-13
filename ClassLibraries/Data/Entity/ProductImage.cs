using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class ProductImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("ProductId")]
        public Product Products { get; set; }

        [Required]
        [Url]
        [StringLength(250, MinimumLength = 10)]
        public string Url { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
