using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;
using Sm.Domain.OrderAgg;

namespace SM.Infrastructure.Repository
{
    public class OrderRepository: BaseRepository<long, Order>, IOrderRepository
    {
        private readonly SmContext _context;
        public OrderRepository(SmContext context) : base(context)
        {
            _context = context;
        }
    }
}