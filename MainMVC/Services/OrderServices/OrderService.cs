using Bogus.DataSets;
using Data.Context;
using Data.Entity;
using MainMVC.Contracts;
using MainMVC.ViewModels.OrderViewModel;
using Microsoft.EntityFrameworkCore;

namespace MainMVC.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _dbContext;

        public OrderService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
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
                OrderCode = GenerateOrderCode() // Bu metot rastgele sipariş kodu oluşturabilir
            };

            _dbContext.Orders.Add(newOrder);
            await _dbContext.SaveChangesAsync();

            return true;
        }


        public async Task<OrderViewModel> GetOrderDetailsAsync(int orderId)
        {
            var order =  _dbContext.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Product)
                .FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                return null;
            }

            var orderViewModel = new OrderViewModel
            {
                OrderId = order.Id,
                UserId = order.UserId,
                UserName = order.User?.UserName,
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
    }
}
