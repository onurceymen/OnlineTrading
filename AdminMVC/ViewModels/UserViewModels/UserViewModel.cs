namespace AdminMVC.ViewModels.UserViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsSeller { get; set; }
        public bool Enabled { get; set; }
    }
}
