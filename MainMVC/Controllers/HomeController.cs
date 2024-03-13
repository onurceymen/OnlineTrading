﻿using MainMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MainMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
