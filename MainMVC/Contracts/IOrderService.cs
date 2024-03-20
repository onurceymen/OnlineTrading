using MainMVC.ViewModels.OrderViewModel;

namespace MainMVC.Contracts
{
    public interface IOrderService
    {
        Task<bool> CreateOrderAsync(OrderViewModel model);
        Task<OrderViewModel> GetOrderDetailsAsync(int orderId);
    }
}
