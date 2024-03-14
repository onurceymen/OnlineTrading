using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("SellerId")]
        public User Seller { get; set; }

        [ForeignKey("CategoryId")]
        public Category Categorys { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [StringLength(1000)]
        public string Details { get; set; }

        [Required]
        public byte StockAmount { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public bool Enabled { get; set; } = true;


        public ICollection<ProductComment> ProductComments { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}
