using Data.Context;
using MainMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MainMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var users = SeedData.GetUser(10);
            return View(users);
        }

        [Route("/about-us")]
        public IActionResult AboutUs()
        {
            // Register action implementation
            return View();
        }

        public IActionResult Contact()
        {
            // Login action implementation
            return View();
        }

        public IActionResult Listing()
        {
            // ForgotPassword action implementation
            return View();
        }

        [Route("{categoryName}-{title}-{id}/details")]
        public IActionResult ProductDetail()
        {
            // Logout action implementation
            return View();
        }
    }
}
