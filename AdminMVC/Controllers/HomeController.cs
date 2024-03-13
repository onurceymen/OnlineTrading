using AdminMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdminMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
