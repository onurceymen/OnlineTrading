// ProfileService.cs
using System;
using System.Linq;
using Data.Context;
using Data.Entity;
using MainMVC.ViewModels.ProfileViewModels;

namespace Data.Services
{
    public class ProfileService
    {
        private readonly AppDbContext _dbContext;

        public ProfileService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ProfileViewModel GetUserProfile(int userId)
        {
            var user = _dbContext.Users
                .Where(u => u.Id == userId)
                .Select(u => new ProfileViewModel
                {
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Role = u.Roles.Name,
                    Enabled = u.Enabled
                })
                .FirstOrDefault();

            return user;
        }

        public void UpdateUserProfile(int userId, ProfileViewModel profileViewModel)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.FirstName = profileViewModel.FirstName;
                user.LastName = profileViewModel.LastName;

                _dbContext.SaveChanges();
            }
        }

        public List<Order> GetUserOrders(int userId)
        {
            return _dbContext.Orders
                .Where(o => o.Users.Id == userId)
                .ToList();
        }

        public List<Product> GetSellerProducts(int sellerId)
        {
            return _dbContext.Products
                .Where(p => p.Seller.Id == sellerId)
                .ToList();
        }
    }
}
