using Data.Constants;
using MainMVC.Contracts;
using MainMVC.Services.OrderServices;
using MainMVC.ViewModels.OrderViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize(Roles = RoleConstant.BuyerRole + RoleConstant.SellerRole)]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = RoleConstant.BuyerRole + RoleConstant.SellerRole)]
        [HttpPost]
        public async Task<IActionResult> Create(OrderViewModel model)
        {
            await _orderService.GetOrderDetailsAsync(model.OrderId);
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = RoleConstant.BuyerRole + RoleConstant.SellerRole)]
        [HttpGet]
        public IActionResult Details()
        {
            return View();
        }

        [Authorize(Roles = RoleConstant.BuyerRole + RoleConstant.SellerRole)]
        [HttpPost]
        public async Task<IActionResult> Details(int id)
        {
            var orderViewModel = await _orderService.GetOrderDetailsAsync(id);
            return View(orderViewModel);
        }

        [Authorize(Roles = RoleConstant.BuyerRole + RoleConstant.SellerRole)]
        [HttpGet]
        public async Task<IActionResult> List(int userId)
        {
            var orderListViewModel = await _orderService.GetOrdersListAsync(userId);
            return View(orderListViewModel);
        }
    }
}
