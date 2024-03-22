using AdminMVC.Services.UserServices;
using Data.Constants;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = RoleConstant.AdminRole)]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = RoleConstant.AdminRole)]
        public IActionResult List()
        {
            return View();
        }

        [Authorize(Roles = RoleConstant.AdminRole)]
        [HttpPost]
        public IActionResult Approve(int userId)
        {
            return View();
        }
    }
}
