namespace MainMVC.ViewModels.OrderViewModel
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string OrderCode { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public byte Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }

}
