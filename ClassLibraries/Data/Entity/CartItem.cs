using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Entity
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Range(1, byte.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        [Required]
        public byte Quantity { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [ForeignKey("UserId")]
        public  User User { get; set; }

        [ForeignKey("ProductId")]
        public  Product Product { get; set; }
    }
}
