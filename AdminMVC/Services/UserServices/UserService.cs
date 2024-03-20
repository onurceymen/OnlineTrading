using AdminMVC.Contracts;
using AdminMVC.ViewModels.UserViewModels;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AdminMVC.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;

        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            return await _dbContext.Users
                           .Select(u => new UserViewModel
                           {
                               UserId = u.Id,
                               Email = u.Email,
                               FirstName = u.FirstName,
                               LastName = u.LastName,
                               IsSeller = u.Role.Name == "Seller", // Assuming Seller role name
                               Enabled = u.Enabled
                           })
                           .ToListAsync();
        }

        public Task<UserViewModel> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserViewModel> CreateUserAsync(UserViewModel user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(UserViewModel user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task ApproveSeller(int userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                // Assume Seller role id is 2
                user.RoleId = 2;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
