namespace Sm.Application.Contracts.Order
{
    public interface IOrderApplication
    {
        long PlaceOrder(myCart cart);
        string PaymentSucceeded(long orderId, long refId);
        public double GetAmountBy(long id);

    }
}