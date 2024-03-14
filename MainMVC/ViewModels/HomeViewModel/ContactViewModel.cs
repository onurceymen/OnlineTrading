using Data.Entity;

namespace MainMVC.ViewModels.HomeViewModel
{
    public class ContactViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Message { get; set; }

    }
}
