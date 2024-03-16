using AdminMVC.ViewModels.UserViewModels;
using Data.Context;

namespace AdminMVC.Services.UserServices
{
    public class UserService
    {
        private readonly AppDbContext _dbContext;

        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<UserViewModel> GetUsers()
        {
            return _dbContext.Users
                .Select(u => new UserViewModel
                {
                    UserId = u.Id,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    IsSeller = u.Roles.Name == "Seller", // Assuming Seller role name
                    Enabled = u.Enabled
                })
                .ToList();
        }

        public void ApproveSeller(int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                // Assume Seller role id is 2
                user.RoleId = 2;
                _dbContext.SaveChanges();
            }
        }
    }
}
