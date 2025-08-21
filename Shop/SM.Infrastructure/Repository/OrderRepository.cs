using System.Linq;
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

        public double GetAmountBy(long id)
        {
            var order = _context.Orders.Select(x => new { x.PayAmount, x.Id }).FirstOrDefault(x => x.Id == id);
            if (order != null)
                return order.PayAmount;
            return 0;
        }
    }
}