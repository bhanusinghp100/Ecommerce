using OrderApi.Models;
using OrderApi.Repository.Interface;
using OrderApi.Services.Interface;

namespace OrderApi.Services
{
    public class OrderService : IOrderService
    {
        private IorderRepository orderService;
        public OrderService(IorderRepository orderService)
        {
            this.orderService = orderService;
        }

        public Task<List<Order>> GetOrdersAsync(CancellationToken cancellationToken)
        {
            return orderService.GetOrdersAsync(cancellationToken);
        }
    }
}
