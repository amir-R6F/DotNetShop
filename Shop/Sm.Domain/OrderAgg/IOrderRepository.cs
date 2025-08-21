using Shop.Domain;

namespace Sm.Domain.OrderAgg
{
    public interface IOrderRepository : IBaseRepository<long, Order>
    {
        double GetAmountBy(long id);
    }
}