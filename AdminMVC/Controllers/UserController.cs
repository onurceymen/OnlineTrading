using AdminMVC.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace AdminMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            var users = _userService.GetUsers();
            return View(users);
        }

        [HttpPost]
        public IActionResult Approve(int userId)
        {
            _userService.ApproveSeller(userId);
            return RedirectToAction("List");
        }
    }
}
