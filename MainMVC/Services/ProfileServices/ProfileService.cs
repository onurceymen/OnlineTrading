// ProfileService.cs
using System;
using System.Linq;
using Data.Context;
using Data.Entity;
using MainMVC.Contracts;
using MainMVC.ViewModels.OrderViewModel;
using MainMVC.ViewModels.ProductViewModel;
using MainMVC.ViewModels.ProfileViewModels;

namespace Data.Services
{
    public class ProfileService : IProfileService
    {
        private readonly AppDbContext _dbContext;

        public ProfileService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProfileViewModel> GetUserProfileAsync(int userId)
        {
            var user = _dbContext.Users
                            .Where(u => u.Id == userId)
                            .Select(u => new ProfileViewModel
                            {
                                Email = u.Email,
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                Role = u.Role.Name,
                                Enabled = u.Enabled
                            })
                            .FirstOrDefault();
            return user;
        }

        public Task<bool> UpdateUserProfileAsync(ProfileViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderViewModel>> GetUserOrdersAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductViewModel>> GetUserProductsAsync(int userId)
        {
            throw new NotImplementedException();
        }

       
    }
}
