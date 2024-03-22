using Bogus.DataSets;
using Data.Context;
using Data.Entity;
using Data.Services;
using MainMVC.Contracts;
using MainMVC.ViewModels.OrderViewModel;
using Microsoft.EntityFrameworkCore;

namespace MainMVC.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly DataRepository<Order> _orderRepository;
        private readonly DataRepository<OrderItem> _orderItemRepository;

        public OrderService(AppDbContext dbContext)
        {
            _orderRepository = new DataRepository<Order>(dbContext);
            _orderItemRepository = new DataRepository<OrderItem>(dbContext);
        }

        private string GenerateOrderCode()
        {
            // Rastgele sipariş kodu oluşturmak için kullanılacak kod
            return "ODR" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }

        public async Task<bool> CreateOrderAsync(OrderViewModel model)
        {
            var newOrder = new Order
            {
                UserId = model.UserId,
                Address = model.Address,
                CreatedAt = DateTime.Now,
                OrderCode = GenerateOrderCode()
            };

            await _orderRepository.CreateAsync(newOrder);

            // Örnek olarak bir OrderItem ekliyorum, gerçek uygulamada sepetten ürünler eklenmelidir.
            var newOrderItem = new OrderItem
            {
                OrderId = newOrder.Id,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                UnitPrice = model.UnitPrice
            };

            await _orderItemRepository.CreateAsync(newOrderItem);

            return true;
        }


        public async Task<OrderViewModel> GetOrderDetailsAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
            {
                return null;
            }

            var orderViewModel = new OrderViewModel
            {
                OrderId = order.Id,
                UserId = order.UserId,
                OrderCode = order.OrderCode,
                Address = order.Address,
                CreatedAt = order.CreatedAt,
                ProductId = order.OrderItems.FirstOrDefault()?.Product?.Id ?? 0,
                ProductName = order.OrderItems.FirstOrDefault()?.Product?.Name,
                UnitPrice = order.OrderItems.FirstOrDefault()?.UnitPrice ?? 0,
                Quantity = order.OrderItems.FirstOrDefault()?.Quantity ?? 0,
                TotalPrice = order.OrderItems.Sum(oi => oi.UnitPrice * oi.Quantity)
            };

            return orderViewModel;
        }

        public async Task<List<OrderViewModel>> GetOrdersListAsync(int userId)
        {
            var orders = await _orderRepository.FindAsync(o => o.UserId == userId);

            var orderViewModels = orders.Select(order => new OrderViewModel
            {
                OrderId = order.Id,
                UserId = order.UserId,
                OrderCode = order.OrderCode,
                Address = order.Address,
                CreatedAt = order.CreatedAt,
                TotalPrice = order.OrderItems.Sum(oi => oi.UnitPrice * oi.Quantity)
            }).ToList();

            return orderViewModels;
        }

    }
}
