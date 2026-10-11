using OrderApi.Models;

namespace OrderApi.Repository.Interface
{
    public interface IorderRepository
    {

        Task<List<Order>> GetOrdersAsync(
        CancellationToken cancellationToken);
    }
}
