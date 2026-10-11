using Microsoft.EntityFrameworkCore;
using OrderApi.Models;
using OrderApi.Repository.Interface;


namespace OrderApi.Repository
{
    public class OrderRepository:IorderRepository
    {
        private readonly EcommerceContext _context;
        public OrderRepository(EcommerceContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetOrdersAsync(
        CancellationToken cancellationToken)
        {
            return await _context.Orders
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
