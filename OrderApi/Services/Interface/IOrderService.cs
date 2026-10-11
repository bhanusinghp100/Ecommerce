using OrderApi.Models;
namespace OrderApi.Services.Interface
{
    

        public interface IOrderService
        {
            Task<List<Order>> GetOrdersAsync(
                CancellationToken cancellationToken);
        }
    
}
