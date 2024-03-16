using Data.Context;
using MainMVC.ViewModels.OrderViewModel;

namespace MainMVC.Services.OrderServices
{
    public class OrderService
    {
        private readonly AppDbContext _dbContext;

        public OrderService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateOrder(int userId, string address)
        {
            var orderCode = Guid.NewGuid().ToString().Substring(0, 8); // Generate a unique order code
            var order = new Order
            {
                UserId = userId,
                OrderCode = orderCode,
                Address = address,
                CreatedAt = DateTime.Now
            };

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
        }

        public OrderViewModel GetOrderDetails(int orderId)
        {
            var order = _dbContext.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                return null;
            }

            var orderItems = _dbContext.OrderItems
                .Where(oi => oi.OrderId == orderId)
                .Select(oi => new OrderItemViewModel
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    UnitPrice = oi.UnitPrice,
                    Quantity = oi.Quantity,
                    TotalPrice = oi.UnitPrice * oi.Quantity
                })
                .ToList();

            var orderViewModel = new OrderViewModel
            {
                OrderId = order.Id,
                UserId = order.UserId,
                UserName = order.User.FirstName + " " + order.User.LastName,
                OrderCode = order.OrderCode,
                Address = order.Address,
                CreatedAt = order.CreatedAt,
                OrderItems = orderItems
            };

            return orderViewModel;
        }
    }
