using Data.Constants;
using Data.Entity;
using Data.Services;
using MainMVC.ViewModels.ProfileViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MainMVC.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ProfileService _profileService;
        private readonly UserManager<User> _userManager;

        public ProfileController(ProfileService profileService, UserManager<User> userManager)
        {
            _profileService = profileService;
            _userManager = userManager;
        }

        [Authorize(Roles = RoleConstant.BuyerRole + "," + RoleConstant.SellerRole + RoleConstant.AdminRole)]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var profileViewModel = new ProfileViewModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role.Name,
                Enabled = user.Enabled
            };

            return View(profileViewModel);
        }

        public async Task<IActionResult> Details()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var profileViewModel = new ProfileViewModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role.Name,
                Enabled = user.Enabled
            };

            return View(profileViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var profileViewModel = new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return View(profileViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProfileViewModel profileViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(profileViewModel);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = profileViewModel.FirstName;
            user.LastName = profileViewModel.LastName;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new Exception("User update failed.");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = _profileService.GetUserOrdersAsync(Convert.ToInt32(userId));

            return View(orders);
        }

        public async Task<IActionResult> MyProducts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var products = _profileService.GetUserProductsAsync(Convert.ToInt32(userId));

            return View(products);
        }
    }
}
