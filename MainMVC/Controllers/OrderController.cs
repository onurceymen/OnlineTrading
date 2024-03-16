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

        public IActionResult Create()
        {
            // Implement view logic as needed
            return View();
        }

        [HttpPost]
        public IActionResult Create(OrderViewModel model)
        {
            // Implement validation and error handling as needed
            _orderService.CreateOrder(model.UserId, model.Address);
            return RedirectToAction("Index", "Home"); // Redirect to home page or order details page
        }

        public IActionResult Details(int id)
        {
            var orderViewModel = _orderService.GetOrderDetails(id);
            if (orderViewModel == null)
            {
                return NotFound(); // or handle the case where order is not found
            }

            return View(orderViewModel);
        }
    }
}
