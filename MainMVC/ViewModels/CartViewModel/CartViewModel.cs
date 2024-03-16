namespace MainMVC.ViewModels.CartViewModel
{
    public class CartViewModel
    {
        public int UserId { get; set; } 
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public byte Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
