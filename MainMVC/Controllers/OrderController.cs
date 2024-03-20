using MainMVC.Services.OrderServices;
using MainMVC.ViewModels.OrderViewModel;
using Microsoft.AspNetCore.Mvc;

namespace MainMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderViewModel model)
        {
            await _orderService.GetOrderDetailsAsync(model.OrderId);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Details()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Details(int id)
        {
            var orderViewModel = await _orderService.GetOrderDetailsAsync(id);
            return View(orderViewModel);
        }
    }
}
