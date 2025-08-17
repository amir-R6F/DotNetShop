namespace Sm.Application.Contracts.Order
{
    public interface IOrderApplication
    {
        long PlaceOrder(myCart cart);
        void PaymentSucceeded(long orderId, long refId);
    }
}