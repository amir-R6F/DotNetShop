using System.Collections.Generic;

namespace Sm.Application.Contracts.Order
{
    public interface IOrderApplication
    {
        long PlaceOrder(myCart cart);
        string PaymentSucceeded(long orderId, long refId);
        public double GetAmountBy(long id);

        void Cancel(long id);

        List<OrderViewModel> Search(OrderSearchModel searchModel);
        List<OrderItemViewModel> GetItems(long orderId);

    }
}