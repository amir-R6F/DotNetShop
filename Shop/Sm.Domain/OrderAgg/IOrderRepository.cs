using System.Collections.Generic;
using Shop.Domain;
using Sm.Application.Contracts.Order;

namespace Sm.Domain.OrderAgg
{
    public interface IOrderRepository : IBaseRepository<long, Order>
    {
        double GetAmountBy(long id);
        List<OrderViewModel> Search(OrderSearchModel searchModel);
        List<OrderItemViewModel> GetItems(long orderId);
    }
}